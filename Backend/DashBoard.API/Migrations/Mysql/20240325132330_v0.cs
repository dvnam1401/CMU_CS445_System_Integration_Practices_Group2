using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DashBoard.API.Migrations.Mysql
{
    public partial class v0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pay rates",
                columns: table => new
                {
                    idPayRates = table.Column<int>(name: "idPay Rates", type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PayRateName = table.Column<string>(name: "Pay Rate Name", type: "varchar(40)", maxLength: 40, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    TaxPercentage = table.Column<decimal>(name: "Tax Percentage", type: "decimal(10,0)", nullable: false),
                    PayType = table.Column<int>(name: "Pay Type", type: "int", nullable: false),
                    PayAmount = table.Column<decimal>(name: "Pay Amount", type: "decimal(10,0)", nullable: false),
                    PTLevelC = table.Column<decimal>(name: "PT - Level C", type: "decimal(10,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pay rates", x => x.idPayRates);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    EmployeeNumber = table.Column<int>(name: "Employee Number", type: "int unsigned", nullable: false),
                    PayRates_idPayRates = table.Column<int>(name: "Pay Rates_idPay Rates", type: "int", nullable: false),
                    idEmployee = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(name: "Last Name", type: "varchar(45)", maxLength: 45, nullable: false),
                    FirstName = table.Column<string>(name: "First Name", type: "varchar(45)", maxLength: 45, nullable: false),
                    SSN = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    PayRate = table.Column<string>(name: "Pay Rate", type: "varchar(40)", maxLength: 40, nullable: true),
                    VacationDays = table.Column<int>(name: "Vacation Days", type: "int", nullable: true),
                    PaidToDate = table.Column<decimal>(name: "Paid To Date", type: "decimal(2,0)", nullable: true),
                    PaidLastYear = table.Column<decimal>(name: "Paid Last Year", type: "decimal(2,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => new { x.EmployeeNumber, x.PayRates_idPayRates });
                    table.ForeignKey(
                        name: "FK_employee_pay rates_Pay Rates_idPay Rates",
                        column: x => x.PayRates_idPayRates,
                        principalTable: "pay rates",
                        principalColumn: "idPay Rates",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "birthday",
                columns: table => new
                {
                    idbirthday = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    dateofbirthday = table.Column<DateTime>(type: "date", nullable: true),
                    employeeNumber = table.Column<int>(type: "int unsigned", nullable: false),
                    EmployeeNumberNavigationEmployeeNumber = table.Column<int>(type: "int unsigned", nullable: true),
                    EmployeeNumberNavigationPayRatesIdPayRates = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_birthday", x => x.idbirthday);
                    table.ForeignKey(
                        name: "FK_birthday_employee_EmployeeNumberNavigationEmployeeNumber_Emp~",
                        columns: x => new { x.EmployeeNumberNavigationEmployeeNumber, x.EmployeeNumberNavigationPayRatesIdPayRates },
                        principalTable: "employee",
                        principalColumns: new[] { "Employee Number", "Pay Rates_idPay Rates" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detail vacation",
                columns: table => new
                {
                    idvacation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    dayoff = table.Column<DateTime>(type: "date", nullable: true),
                    resignationcontent = table.Column<string>(name: "resignation content", type: "varchar(500)", maxLength: 500, nullable: true),
                    employeeNumber = table.Column<int>(type: "int unsigned", nullable: false),
                    EmployeeNumberNavigationEmployeeNumber = table.Column<int>(type: "int unsigned", nullable: true),
                    EmployeeNumberNavigationPayRatesIdPayRates = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail vacation", x => x.idvacation);
                    table.ForeignKey(
                        name: "FK_detail vacation_employee_EmployeeNumberNavigationEmployeeNum~",
                        columns: x => new { x.EmployeeNumberNavigationEmployeeNumber, x.EmployeeNumberNavigationPayRatesIdPayRates },
                        principalTable: "employee",
                        principalColumns: new[] { "Employee Number", "Pay Rates_idPay Rates" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "employeeNumber",
                table: "birthday",
                column: "employeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_birthday_EmployeeNumberNavigationEmployeeNumber_EmployeeNumb~",
                table: "birthday",
                columns: new[] { "EmployeeNumberNavigationEmployeeNumber", "EmployeeNumberNavigationPayRatesIdPayRates" });

            migrationBuilder.CreateIndex(
                name: "employeeNumber1",
                table: "detail vacation",
                column: "employeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_detail vacation_EmployeeNumberNavigationEmployeeNumber_Emplo~",
                table: "detail vacation",
                columns: new[] { "EmployeeNumberNavigationEmployeeNumber", "EmployeeNumberNavigationPayRatesIdPayRates" });

            migrationBuilder.CreateIndex(
                name: "Employee Number_UNIQUE",
                table: "employee",
                column: "Employee Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_Employee_Pay Rates",
                table: "employee",
                column: "Pay Rates_idPay Rates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "birthday");

            migrationBuilder.DropTable(
                name: "detail vacation");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "pay rates");
        }
    }
}
