using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP_LMS_ASP.Net8_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupPaymentCycles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupPaymentCycles");
        }
    }
}
