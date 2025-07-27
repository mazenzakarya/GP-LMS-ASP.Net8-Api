using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP_LMS_ASP.Net8_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");
        }
    }
}