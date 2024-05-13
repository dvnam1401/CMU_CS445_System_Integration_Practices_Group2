using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations.Admin
{
    /// <inheritdoc />
    public partial class createV0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "UserGroups",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "UserGroups",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<int>(
                name: "permission_id",
                table: "Permissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "permission_id",
                table: "GroupPermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "GroupPermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "AccountUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "group_id",
                table: "UserGroups",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "user_id",
                table: "UserGroups",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "permission_id",
                table: "Permissions",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "group_id",
                table: "Groups",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "permission_id",
                table: "GroupPermissions",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "group_id",
                table: "GroupPermissions",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "user_id",
                table: "AccountUser",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
