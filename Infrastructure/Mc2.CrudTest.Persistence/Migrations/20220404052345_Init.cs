using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BankAccountNumber", "DateOfBirth", "Email", "Firstname", "Lastname", "PhoneNumber" },
                values: new object[] { 1, "123456", new DateTime(1981, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hamidnch2007@gmail.com", "Hamid", "NCH", "09124820700" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BankAccountNumber", "DateOfBirth", "Email", "Firstname", "Lastname", "PhoneNumber" },
                values: new object[] { 2, "3251388", new DateTime(2001, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "RazaviAli@gmail.com", "Ali", "Razavi", "09123526532" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Firstname_Lastname_DateOfBirth",
                table: "Customers",
                columns: new[] { "Firstname", "Lastname", "DateOfBirth" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mc2_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
