using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenefitPlans",
                columns: table => new
                {
                    BenefitPlanID = table.Column<decimal>(name: "Benefit_Plan_ID", type: "decimal(18,2)", nullable: false),
                    PlanName = table.Column<string>(name: "Plan_Name", type: "nvarchar(max)", nullable: false),
                    Deductable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentageCoPay = table.Column<int>(name: "Percentage_CoPay", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitPlans", x => x.BenefitPlanID);
                });

            migrationBuilder.CreateTable(
                name: "Pay Rates",
                columns: table => new
                {
                    idPayRates = table.Column<int>(name: "idPay Rates", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayRateName = table.Column<string>(name: "Pay Rate Name", type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxPercentage = table.Column<decimal>(name: "Tax Percentage", type: "decimal(18,2)", nullable: false),
                    PayType = table.Column<int>(name: "Pay Type", type: "int", nullable: false),
                    PayAmount = table.Column<decimal>(name: "Pay Amount", type: "decimal(18,2)", nullable: false),
                    PTLevelC = table.Column<decimal>(name: "PT - Level C", type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay Rates", x => x.idPayRates);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    EmployeeID = table.Column<decimal>(name: "Employee_ID", type: "decimal(18,2)", nullable: false),
                    FirstName = table.Column<string>(name: "First_Name", type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(name: "Last_Name", type: "nvarchar(50)", nullable: false),
                    MiddleInitial = table.Column<string>(name: "Middle_Initial", type: "nvarchar(50)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Zip = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(name: "Phone_Number", type: "nvarchar(50)", nullable: false),
                    SocialSecurityNumber = table.Column<string>(name: "Social_Security_Number", type: "nvarchar(50)", nullable: false),
                    DriversLicense = table.Column<string>(name: "Drivers_License", type: "nvarchar(50)", nullable: false),
                    MaritalStatus = table.Column<string>(name: "Marital_Status", type: "nvarchar(50)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    ShareholderStatus = table.Column<bool>(name: "Shareholder_Status", type: "bit", nullable: true),
                    BenefitPlans = table.Column<decimal>(name: "Benefit_Plans", type: "decimal(18,2)", nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BenefitPlanbenefitId = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Personal_BenefitPlans_BenefitPlanbenefitId",
                        column: x => x.BenefitPlanbenefitId,
                        principalTable: "BenefitPlans",
                        principalColumn: "Benefit_Plan_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    idEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<int>(name: "Employee Number", type: "int", nullable: false),
                    LastName = table.Column<string>(name: "Last Name", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    SSN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayRate = table.Column<string>(name: "Pay Rate", type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PayRatesidPayRates = table.Column<int>(name: "Pay Rates_idPay Rates", type: "int", nullable: false),
                    VacationDays = table.Column<int>(name: "Vacation Days", type: "int", nullable: false),
                    PaidToDate = table.Column<decimal>(name: "Paid To Date", type: "decimal(18,2)", nullable: false),
                    PaidLastYear = table.Column<decimal>(name: "Paid Last Year", type: "decimal(18,2)", nullable: false),
                    PayRatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.idEmployee);
                    table.ForeignKey(
                        name: "FK_Employee_Pay Rates_PayRatesId",
                        column: x => x.PayRatesId,
                        principalTable: "Pay Rates",
                        principalColumn: "idPay Rates",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emergency_Contacts",
                columns: table => new
                {
                    EmployeeID = table.Column<decimal>(name: "Employee_ID", type: "decimal(18,2)", nullable: false),
                    EmergencyContactName = table.Column<string>(name: "Emergency_Contact_Name", type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(name: "Phone_Number", type: "nvarchar(50)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emergency_Contacts", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Emergency_Contacts_Personal_Employee_ID",
                        column: x => x.EmployeeID,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employment",
                columns: table => new
                {
                    EmployeeId = table.Column<decimal>(name: "Employee_Id", type: "decimal(18,2)", nullable: false),
                    EmploymentStatus = table.Column<string>(name: "Employment_Status", type: "nvarchar(50)", nullable: false),
                    HireDate = table.Column<DateTime>(name: "Hire_Date", type: "datetime2", nullable: true),
                    WorkersCompCode = table.Column<string>(name: "Workers_Comp_Code", type: "nvarchar(50)", nullable: false),
                    TerminationDate = table.Column<DateTime>(name: "Termination_Date", type: "datetime2", nullable: true),
                    RehireDate = table.Column<DateTime>(name: "Rehire_Date", type: "datetime2", nullable: true),
                    LastReviewDate = table.Column<DateTime>(name: "Last_Review_Date", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employment_Personal_Employee_Id",
                        column: x => x.EmployeeId,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job_History",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<decimal>(name: "Employee_ID", type: "decimal(18,2)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StartDate = table.Column<DateTime>(name: "Start_Date", type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(name: "End_Date", type: "datetime2", nullable: true),
                    JobTitle = table.Column<string>(name: "Job_Title", type: "nvarchar(50)", nullable: false),
                    Supervisor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    JobCategory = table.Column<string>(name: "Job_Category", type: "nvarchar(50)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DepartmenCode = table.Column<decimal>(name: "Departmen_Code", type: "decimal(18,2)", nullable: true),
                    SalaryType = table.Column<decimal>(name: "Salary_Type", type: "decimal(18,2)", nullable: true),
                    PayPeriod = table.Column<decimal>(name: "Pay_Period", type: "decimal(18,2)", nullable: true),
                    HoursperWeek = table.Column<decimal>(name: "Hours_per_Week", type: "decimal(18,2)", nullable: true),
                    HazardousTraining = table.Column<bool>(name: "Hazardous_Training", type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_History", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Job_History_Personal_Employee_ID",
                        column: x => x.EmployeeID,
                        principalTable: "Personal",
                        principalColumn: "Employee_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PayRatesId",
                table: "Employee",
                column: "PayRatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_History_Employee_ID",
                table: "Job_History",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_BenefitPlanbenefitId",
                table: "Personal",
                column: "BenefitPlanbenefitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emergency_Contacts");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Employment");

            migrationBuilder.DropTable(
                name: "Job_History");

            migrationBuilder.DropTable(
                name: "Pay Rates");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "BenefitPlans");
        }
    }
}
