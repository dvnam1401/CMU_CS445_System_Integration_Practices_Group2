using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations.Admin
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_user_id",
                table: "UserPermissions",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AccountUser_user_id",
                table: "UserPermissions",
                column: "user_id",
                principalTable: "AccountUser",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AccountUser_user_id",
                table: "UserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_user_id",
                table: "UserPermissions");
        }
    }
}
