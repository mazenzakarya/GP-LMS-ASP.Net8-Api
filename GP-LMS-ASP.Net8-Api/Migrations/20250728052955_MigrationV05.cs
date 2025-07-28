using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_GroupPaymentCycles_PaymentCycleId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Levels_LevelId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupPaymentCycles");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Groups_LevelId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Fees_PaymentCycleId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Groups");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                table: "StudentGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsExcepctionSession",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropColumn(
                name: "LastPaymentDate",
                table: "StudentGroups");

            migrationBuilder.DropColumn(
                name: "IsExcepctionSession",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupPaymentCycles",
                columns: table => new
                {
                    GroupPaymentCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    SessionsCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPaymentCycles", x => x.GroupPaymentCycleId);
                    table.ForeignKey(
                        name: "FK_GroupPaymentCycles_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupsId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LevelId",
                table: "Groups",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_PaymentCycleId",
                table: "Fees",
                column: "PaymentCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPaymentCycles_GroupId",
                table: "GroupPaymentCycles",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_GroupPaymentCycles_PaymentCycleId",
                table: "Fees",
                column: "PaymentCycleId",
                principalTable: "GroupPaymentCycles",
                principalColumn: "GroupPaymentCycleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Levels_LevelId",
                table: "Groups",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
