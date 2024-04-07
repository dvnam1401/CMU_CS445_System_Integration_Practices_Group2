using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations.Mysql
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~",
                table: "detail vacation");

            migrationBuilder.DropForeignKey(
                name: "FK_detail vacation_employee_employeeNumber",
                table: "detail vacation");

            migrationBuilder.DropIndex(
                name: "IX_detail vacation_EmployeeNumberNavigationEmployeeNumber",
                table: "detail vacation");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber1",
                table: "detail vacation");

            migrationBuilder.DropColumn(
                name: "EmployeeNumberNavigationEmployeeNumber",
                table: "detail vacation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "EmployeeNumber1",
                table: "detail vacation",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "EmployeeNumberNavigationEmployeeNumber",
                table: "detail vacation",
                type: "int unsigned",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_detail vacation_EmployeeNumberNavigationEmployeeNumber",
                table: "detail vacation",
                column: "EmployeeNumberNavigationEmployeeNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~",
                table: "detail vacation",
                column: "EmployeeNumberNavigationEmployeeNumber",
                principalTable: "employee",
                principalColumn: "Employee Number");

            migrationBuilder.AddForeignKey(
                name: "FK_detail vacation_employee_employeeNumber",
                table: "detail vacation",
                column: "employeeNumber",
                principalTable: "employee",
                principalColumn: "Employee Number",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
