using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP_LMS_ASP.Net8_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Fees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUser",
                table: "Fees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "UpdatedByUser",
                table: "Fees");
        }
    }
}
