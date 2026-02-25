using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectUsersAndFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserId",
                table: "ProjectUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssigneeId", "CreatedAt", "CreatedBy", "Description", "DueDate", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7482), new Guid("00000000-0000-0000-0000-000000000001"), "ChongHsien Task Description", new DateTime(2026, 2, 26, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7478), 0, 0, "ChongHsien Task", new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7482) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7486), new Guid("00000000-0000-0000-0000-000000000002"), "YeeChian Task Description", new DateTime(2026, 2, 27, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7486), 1, 1, "YeeChian Task", new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7487) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7521), new Guid("00000000-0000-0000-0000-000000000003"), "Falcon Task Description", new DateTime(2026, 2, 28, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7521), 2, 2, "Falcon Task", new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7522) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7529), new Guid("00000000-0000-0000-0000-000000000001"), "ChongHsien toFalcon Task Description", new DateTime(2026, 3, 1, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7529), 0, 4, "ChongHsien to Falcon Task", new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7530) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7533), new Guid("00000000-0000-0000-0000-000000000002"), "YeeChian Task Description", new DateTime(2026, 3, 2, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7533), 2, 3, "YeeChian to Falcon Task", new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7534) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7384), "ChongHsien", 0, "ChongHsien" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7387), "YeeChian", 1, "YeeChian" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7388), "Falcon", 2, "Falcon" }
                });
        }
    }
}
