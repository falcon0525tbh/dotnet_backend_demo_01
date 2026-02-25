using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class deleteTestTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7666), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 18, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7658), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7666) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7675), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 19, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7674), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7676) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7682), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 2, 20, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7682), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7683) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7692), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 2, 21, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7691), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7693) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7701), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 2, 22, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 2, 11, 4, 27, 7, 158, DateTimeKind.Utc).AddTicks(7701) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2891), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 2, 17, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2885), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2891) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2895), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 2, 18, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2895), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2896) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2899), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 2, 19, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2899), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2904), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 2, 20, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2903), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2904) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "CreatedBy", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2916), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 2, 21, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2915), new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2916) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2769));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2773));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 10, 4, 52, 30, 942, DateTimeKind.Utc).AddTicks(2783));
        }
    }
}
