using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP_LMS_ASP.Net8_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_Groups_GroupsId",
                table: "CourseSubjectElements");

            migrationBuilder.DropIndex(
                name: "IX_CourseSubjectElements_GroupsId",
                table: "CourseSubjectElements");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                table: "CourseSubjectElements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsId",
                table: "CourseSubjectElements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubjectElements_GroupsId",
                table: "CourseSubjectElements",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_Groups_GroupsId",
                table: "CourseSubjectElements",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "GroupsId");
        }
    }
}
