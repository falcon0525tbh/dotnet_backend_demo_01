using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssigneeId", "CreatedAt", "CreatedBy", "Description", "DueDate", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2891), new Guid("00000000-0000-0000-0000-000000000000"), "ChongHsien Task Description", new DateTime(2026, 2, 17, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2885), 0, 0, "ChongHsien Task", new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2891) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2895), new Guid("00000000-0000-0000-0000-000000000000"), "YeeChian Task Description", new DateTime(2026, 2, 18, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2895), 1, 1, "YeeChian Task", new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2896) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2899), new Guid("00000000-0000-0000-0000-000000000000"), "Falcon Task Description", new DateTime(2026, 2, 19, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2899), 2, 2, "Falcon Task", new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2900) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2904), new Guid("00000000-0000-0000-0000-000000000000"), "ChongHsien toFalcon Task Description", new DateTime(2026, 2, 20, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2903), 0, 4, "ChongHsien to Falcon Task", new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2904) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2916), new Guid("00000000-0000-0000-0000-000000000000"), "YeeChian Task Description", new DateTime(2026, 2, 21, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2915), 2, 3, "YeeChian to Falcon Task", new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2916) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2769), "ChongHsien", 0, "ChongHsien" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2773), "YeeChian", 1, "YeeChian" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2783), "Falcon", 2, "Falcon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
