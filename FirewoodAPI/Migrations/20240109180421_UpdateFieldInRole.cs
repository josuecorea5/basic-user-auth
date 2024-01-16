using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirewoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldInRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 388, DateTimeKind.Utc).AddTicks(1466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 3, 10, 935, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 388, DateTimeKind.Utc).AddTicks(1126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 3, 10, 935, DateTimeKind.Utc).AddTicks(741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "rol",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 387, DateTimeKind.Utc).AddTicks(9871),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "rol",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 387, DateTimeKind.Utc).AddTicks(9313),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 3, 10, 935, DateTimeKind.Utc).AddTicks(1033),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 388, DateTimeKind.Utc).AddTicks(1466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 9, 18, 3, 10, 935, DateTimeKind.Utc).AddTicks(741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 388, DateTimeKind.Utc).AddTicks(1126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "rol",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 387, DateTimeKind.Utc).AddTicks(9871));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "rol",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 9, 18, 4, 21, 387, DateTimeKind.Utc).AddTicks(9313));
        }
    }
}
