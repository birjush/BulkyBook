using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class adminRecordCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "CompanyId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "StreetAddress", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d3757d43-8ef8-445a-b8ce-d0374b4a5255", 0, "ramol", 1, "c1921299-d684-4f0d-a1a4-a2333252dc65", "ApplicationUser", "admin@gmail.com", false, false, null, "bittoo", null, null, "AQAAAAIAAYagAAAAEAfORq60M6FmGMWrtC4hLreopWTaS9UkGo1u+XbPjlfVoyoDBkIqIn/G9e2lVn0PSA==", "1234567890", false, "387710", "db639868-3b4c-4d0b-89a3-fb323e9f1265", "gj", "rml", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3757d43-8ef8-445a-b8ce-d0374b4a5255");
        }
    }
}
