using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashBoard.API.Migrations.Auth
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3da10d72-5354-44fa-9d52-f7f157e55e37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "030da32c-0543-4508-9866-975f86fb1834", "AQAAAAIAAYagAAAAEIXtjHR9tuopD45cSd3PZMl901DKYXhYBtSxD4HhxlYgFjRsyKJ0J2+ng+SyhyHeCw==", "fcda1ede-995c-4d72-980e-611369ef5374" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d462fc9-497d-4001-9f19-5b7b160cd0cb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "693e86dc-e6f9-4eda-a39b-5ebaf034348b", "AQAAAAIAAYagAAAAEBBUc3pOu/7HQ1YmUR0cXeVg9rZ/DNDirG/XbxTJMOhAI1olMYOWyHGFxbTGD1aTpg==", "8d264571-1f03-451c-8e0a-0e37c516842a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7779e2e-3942-4633-a3dd-8bd1051497ab",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c21881c1-b523-4fa9-ad68-7eeeecd092d2", "AQAAAAIAAYagAAAAEAMSkmY8IfOZfsEXLm/A5S5uJaDNJUHT3rm7qtUY2JZpcVRP+L/7PeIMb3LB9teCBQ==", "dbc0dcad-abd5-4ad7-9f16-8430d8747fb8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3da10d72-5354-44fa-9d52-f7f157e55e37",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "addc013d-68e7-4ada-906f-af206a68b0ef", null, "cf569eb6-3e8b-4f39-8f5b-3831f1beb0b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d462fc9-497d-4001-9f19-5b7b160cd0cb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0a28742-6614-47d4-b182-bd5852a1e226", null, "965602c6-8db0-4d20-8031-8ac23b18336e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7779e2e-3942-4633-a3dd-8bd1051497ab",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0f082cd-d0ee-4100-83a0-fe9529c0cfdd", "AQAAAAIAAYagAAAAEDpV2nsh7iuCiOuYmL7UPQaMDSF/eOPt/BWiNRarhaaUvsHTGPENsls4pwtISH433g==", "85a48b37-b8b1-49d5-9c92-e1e0cee93cd0" });
        }
    }
}
