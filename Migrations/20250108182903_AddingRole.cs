using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: ["Id", "ConcurrencyStamp", "Name", "NormalizedName"],
                values: new object[,]
                {
                    { "7d8ad738-3545-4e7c-ac47-1eb2c9fcdd08", null, "admin", "admin" },
                    { "9e857c50-b83d-4bbc-b001-8504596c8de5", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d8ad738-3545-4e7c-ac47-1eb2c9fcdd08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e857c50-b83d-4bbc-b001-8504596c8de5");
        }
    }
}
