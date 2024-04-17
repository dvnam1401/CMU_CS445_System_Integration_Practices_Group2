using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "birthday");

            migrationBuilder.DropTable(
                name: "detail vacation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "birthday",
                columns: table => new
                {
                    employeeNumber = table.Column<uint>(type: "int unsigned", nullable: false),
                    dateofbirthday = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_birthday", x => x.employeeNumber);
                    table.ForeignKey(
                        name: "FK_birthday_employee_employeeNumber",
                        column: x => x.employeeNumber,
                        principalTable: "employee",
                        principalColumn: "Employee Number",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detail vacation",
                columns: table => new
                {
                    VacationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Dayoff = table.Column<DateTime>(type: "date", nullable: true),
                    employeeNumber = table.Column<uint>(type: "int unsigned", nullable: false),
                    resignationcontent = table.Column<string>(name: "resignation content", type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail vacation", x => x.VacationID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "employeeNumber",
                table: "detail vacation",
                column: "employeeNumber");
        }
    }
}
