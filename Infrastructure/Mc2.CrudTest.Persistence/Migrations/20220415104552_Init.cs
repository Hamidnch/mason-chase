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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BankAccountNumber", "DateOfBirth", "Email", "Firstname", "Lastname", "PhoneNumber" },
                values: new object[] { new Guid("a3d91ba2-e1a0-4134-badf-d5fb304fd990"), "123456", new DateTime(1981, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hamidnch2007@gmail.com", "Hamid", "NCH", 9124820700m });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BankAccountNumber", "DateOfBirth", "Email", "Firstname", "Lastname", "PhoneNumber" },
                values: new object[] { new Guid("a43371d0-00a4-4320-9e1d-f19ea81fc651"), "3251388", new DateTime(2001, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "RazaviAli@gmail.com", "Ali", "Razavi", 9123526532m });

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
