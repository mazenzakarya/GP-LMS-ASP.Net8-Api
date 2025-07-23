using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP_LMS_ASP.Net8_Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Groups_GroupId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_StudentId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubject_Course_CourseId",
                table: "CourseSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_CourseSubject_SubjectId",
                table: "CourseSubjectElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_Course_CourseId",
                table: "CourseSubjectElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Course_CourseId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_GroupPaymentCycle_PaymentCycleId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Groups_GroupId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Users_StudentId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_CourseSubjectElements_ElementId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_CourseSubject_SubjectId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Course_CourseId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Users_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPaymentCycle_Groups_GroupId",
                table: "GroupPaymentCycle");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Course_CourseId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Level_LevelId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroup_Groups_GroupId",
                table: "StudentGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroup_Users_StudentId",
                table: "StudentGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPaymentCycle",
                table: "GroupPaymentCycle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grade",
                table: "Grade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fee",
                table: "Fee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSubject",
                table: "CourseSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "StudentGroup",
                newName: "StudentGroups");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "Levels");

            migrationBuilder.RenameTable(
                name: "GroupPaymentCycle",
                newName: "GroupPaymentCycles");

            migrationBuilder.RenameTable(
                name: "Grade",
                newName: "Grades");

            migrationBuilder.RenameTable(
                name: "Fee",
                newName: "Fees");

            migrationBuilder.RenameTable(
                name: "CourseSubject",
                newName: "CourseSubjects");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Attendances");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroup_StudentId",
                table: "StudentGroups",
                newName: "IX_StudentGroups_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroup_GroupId",
                table: "StudentGroups",
                newName: "IX_StudentGroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPaymentCycle_GroupId",
                table: "GroupPaymentCycles",
                newName: "IX_GroupPaymentCycles_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_SubjectId",
                table: "Grades",
                newName: "IX_Grades_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_StudentId",
                table: "Grades",
                newName: "IX_Grades_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_ElementId",
                table: "Grades",
                newName: "IX_Grades_ElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_CourseId",
                table: "Grades",
                newName: "IX_Grades_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Fee_StudentId",
                table: "Fees",
                newName: "IX_Fees_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Fee_PaymentCycleId",
                table: "Fees",
                newName: "IX_Fees_PaymentCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_Fee_GroupId",
                table: "Fees",
                newName: "IX_Fees_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Fee_CourseId",
                table: "Fees",
                newName: "IX_Fees_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSubject_CourseId",
                table: "CourseSubjects",
                newName: "IX_CourseSubjects_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_UserId",
                table: "Attendances",
                newName: "IX_Attendances_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_GroupId",
                table: "Attendances",
                newName: "IX_Attendances_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroups",
                table: "StudentGroups",
                column: "StudentGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "LevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPaymentCycles",
                table: "GroupPaymentCycles",
                column: "GroupPaymentCycleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fees",
                table: "Fees",
                column: "FeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSubjects",
                table: "CourseSubjects",
                column: "CourseSubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Groups_GroupId",
                table: "Attendances",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_CourseSubjects_SubjectId",
                table: "CourseSubjectElements",
                column: "SubjectId",
                principalTable: "CourseSubjects",
                principalColumn: "CourseSubjectId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjects_Courses_CourseId",
                table: "CourseSubjects",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Courses_CourseId",
                table: "Fees",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_GroupPaymentCycles_PaymentCycleId",
                table: "Fees",
                column: "PaymentCycleId",
                principalTable: "GroupPaymentCycles",
                principalColumn: "GroupPaymentCycleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Groups_GroupId",
                table: "Fees",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Users_StudentId",
                table: "Fees",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseSubjectElements_ElementId",
                table: "Grades",
                column: "ElementId",
                principalTable: "CourseSubjectElements",
                principalColumn: "CourseSubjectElementsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseSubjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "CourseSubjects",
                principalColumn: "CourseSubjectId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Users_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPaymentCycles_Groups_GroupId",
                table: "GroupPaymentCycles",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Levels_LevelId",
                table: "Groups",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Groups_GroupId",
                table: "StudentGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Users_StudentId",
                table: "StudentGroups",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Groups_GroupId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_StudentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_UserId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_CourseSubjects_SubjectId",
                table: "CourseSubjectElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjectElements_Courses_CourseId",
                table: "CourseSubjectElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubjects_Courses_CourseId",
                table: "CourseSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Courses_CourseId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_GroupPaymentCycles_PaymentCycleId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Groups_GroupId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Users_StudentId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseSubjectElements_ElementId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseSubjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Users_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPaymentCycles_Groups_GroupId",
                table: "GroupPaymentCycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Courses_CourseId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Levels_LevelId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Groups_GroupId",
                table: "StudentGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Users_StudentId",
                table: "StudentGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroups",
                table: "StudentGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPaymentCycles",
                table: "GroupPaymentCycles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fees",
                table: "Fees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSubjects",
                table: "CourseSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "StudentGroups",
                newName: "StudentGroup");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Level");

            migrationBuilder.RenameTable(
                name: "GroupPaymentCycles",
                newName: "GroupPaymentCycle");

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "Grade");

            migrationBuilder.RenameTable(
                name: "Fees",
                newName: "Fee");

            migrationBuilder.RenameTable(
                name: "CourseSubjects",
                newName: "CourseSubject");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "Attendance");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroup",
                newName: "IX_StudentGroup_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroups_GroupId",
                table: "StudentGroup",
                newName: "IX_StudentGroup_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupPaymentCycles_GroupId",
                table: "GroupPaymentCycle",
                newName: "IX_GroupPaymentCycle_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_SubjectId",
                table: "Grade",
                newName: "IX_Grade_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentId",
                table: "Grade",
                newName: "IX_Grade_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_ElementId",
                table: "Grade",
                newName: "IX_Grade_ElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_CourseId",
                table: "Grade",
                newName: "IX_Grade_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_StudentId",
                table: "Fee",
                newName: "IX_Fee_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_PaymentCycleId",
                table: "Fee",
                newName: "IX_Fee_PaymentCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_GroupId",
                table: "Fee",
                newName: "IX_Fee_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Fees_CourseId",
                table: "Fee",
                newName: "IX_Fee_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSubjects_CourseId",
                table: "CourseSubject",
                newName: "IX_CourseSubject_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_UserId",
                table: "Attendance",
                newName: "IX_Attendance_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendance",
                newName: "IX_Attendance_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_GroupId",
                table: "Attendance",
                newName: "IX_Attendance_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup",
                column: "StudentGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "LevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPaymentCycle",
                table: "GroupPaymentCycle",
                column: "GroupPaymentCycleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grade",
                table: "Grade",
                column: "GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fee",
                table: "Fee",
                column: "FeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSubject",
                table: "CourseSubject",
                column: "CourseSubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Groups_GroupId",
                table: "Attendance",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Users_StudentId",
                table: "Attendance",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Users_UserId",
                table: "Attendance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubject_Course_CourseId",
                table: "CourseSubject",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_CourseSubject_SubjectId",
                table: "CourseSubjectElements",
                column: "SubjectId",
                principalTable: "CourseSubject",
                principalColumn: "CourseSubjectId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubjectElements_Course_CourseId",
                table: "CourseSubjectElements",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Course_CourseId",
                table: "Fee",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_GroupPaymentCycle_PaymentCycleId",
                table: "Fee",
                column: "PaymentCycleId",
                principalTable: "GroupPaymentCycle",
                principalColumn: "GroupPaymentCycleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Groups_GroupId",
                table: "Fee",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Users_StudentId",
                table: "Fee",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_CourseSubjectElements_ElementId",
                table: "Grade",
                column: "ElementId",
                principalTable: "CourseSubjectElements",
                principalColumn: "CourseSubjectElementsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_CourseSubject_SubjectId",
                table: "Grade",
                column: "SubjectId",
                principalTable: "CourseSubject",
                principalColumn: "CourseSubjectId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Course_CourseId",
                table: "Grade",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Users_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPaymentCycle_Groups_GroupId",
                table: "GroupPaymentCycle",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Course_CourseId",
                table: "Groups",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Level_LevelId",
                table: "Groups",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroup_Groups_GroupId",
                table: "StudentGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroup_Users_StudentId",
                table: "StudentGroup",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
