using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DashBoard.API.Migrations
{
    public partial class v0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benefit_Plans",
                columns: table => new
                {
                    Benefit_Plan_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    Plan_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deductable = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Percentage_CoPay = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefit_Plans", x => x.Benefit_Plan_ID);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Employee_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Middle_Initial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Zip = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Social_Security_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Drivers_License = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Marital_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Shareholder_Status = table.Column<bool>(type: "bit", nullable: false),
                    Benefit_Plans = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Employee_ID);
                    table.ForeignKey(
                        name: "FK_Personal_Benefit_Plans_Benefit_Plans",
                        column: x => x.Benefit_Plans,
                        principalTable: "Benefit_Plans",
                        principalColumn: "Benefit_Plan_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emergency_Contacts",
                columns: table => new
                {
                    Employee_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    Emergency_Contact_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emergency_Contacts", x => x.Employee_ID);
                    table.ForeignKey(
                        name: "FK_Emergency_Contacts_Personal_Employee_ID",
                        column: x => x.Employee_ID,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employment",
                columns: table => new
                {
                    Employee_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    Employment_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hire_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Workers_Comp_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Termination_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Rehire_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Last_Review_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment", x => x.Employee_ID);
                    table.ForeignKey(
                        name: "FK_Employment_Personal_Employee_ID",
                        column: x => x.Employee_ID,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job_History",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    Employee_ID = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Division = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    End_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Job_Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Supervisor = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Job_Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Departmen_Code = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Salary_Type = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Pay_Period = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hours_per_Week = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    Hazardous_Training = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_History", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Job_History_Personal_Employee_ID",
                        column: x => x.Employee_ID,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID",
                table: "Emergency_Contacts",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID1",
                table: "Employment",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID2",
                table: "Job_History",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Benefit_Plans",
                table: "Personal",
                column: "Benefit_Plans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emergency_Contacts");

            migrationBuilder.DropTable(
                name: "Employment");

            migrationBuilder.DropTable(
                name: "Job_History");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Benefit_Plans");
        }
    }
}
