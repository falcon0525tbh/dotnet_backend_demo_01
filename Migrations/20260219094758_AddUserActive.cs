using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7482), new DateTime(2026, 2, 26, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7478), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7482) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7486), new DateTime(2026, 2, 27, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7486), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7487) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7521), new DateTime(2026, 2, 28, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7521), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7522) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7529), new DateTime(2026, 3, 1, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7529), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7533), new DateTime(2026, 3, 2, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7533), new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7534) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Active", "CreatedAt" },
                values: new object[] { true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7384) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "Active", "CreatedAt" },
                values: new object[] { true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7387) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Active", "CreatedAt" },
                values: new object[] { true, new DateTime(2026, 2, 19, 9, 47, 58, 773, DateTimeKind.Utc).AddTicks(7388) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7666), new DateTime(2026, 2, 18, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7658), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7666) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7675), new DateTime(2026, 2, 19, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7674), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7676) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7682), new DateTime(2026, 2, 20, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7682), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7683) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7692), new DateTime(2026, 2, 21, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7691), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7693) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7701), new DateTime(2026, 2, 22, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7281));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7305));
        }
    }
}
