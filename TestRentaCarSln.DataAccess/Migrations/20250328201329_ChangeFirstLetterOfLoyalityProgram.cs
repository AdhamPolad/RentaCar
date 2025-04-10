using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class ChangeFirstLetterOfLoyalityProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loyalityPrograms_Customers_CustomerId",
                table: "loyalityPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loyalityPrograms",
                table: "loyalityPrograms");

            migrationBuilder.RenameTable(
                name: "loyalityPrograms",
                newName: "LoyalityPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_loyalityPrograms_CustomerId",
                table: "LoyalityPrograms",
                newName: "IX_LoyalityPrograms_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyalityPrograms",
                table: "LoyalityPrograms",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 13, 29, 475, DateTimeKind.Utc).AddTicks(3538));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 13, 29, 475, DateTimeKind.Utc).AddTicks(3543));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 13, 29, 475, DateTimeKind.Utc).AddTicks(3544));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 13, 29, 475, DateTimeKind.Utc).AddTicks(3545));

            migrationBuilder.AddForeignKey(
                name: "FK_LoyalityPrograms_Customers_CustomerId",
                table: "LoyalityPrograms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyalityPrograms_Customers_CustomerId",
                table: "LoyalityPrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyalityPrograms",
                table: "LoyalityPrograms");

            migrationBuilder.RenameTable(
                name: "LoyalityPrograms",
                newName: "loyalityPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_LoyalityPrograms_CustomerId",
                table: "loyalityPrograms",
                newName: "IX_loyalityPrograms_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loyalityPrograms",
                table: "loyalityPrograms",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 11, 11, 667, DateTimeKind.Utc).AddTicks(4269));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 11, 11, 667, DateTimeKind.Utc).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 11, 11, 667, DateTimeKind.Utc).AddTicks(4276));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 28, 20, 11, 11, 667, DateTimeKind.Utc).AddTicks(4277));

            migrationBuilder.AddForeignKey(
                name: "FK_loyalityPrograms_Customers_CustomerId",
                table: "loyalityPrograms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
