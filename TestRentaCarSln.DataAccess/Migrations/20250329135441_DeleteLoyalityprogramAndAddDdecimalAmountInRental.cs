using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class DeleteLoyalityprogramAndAddDdecimalAmountInRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyalityPrograms");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Rentals",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "DiscountsCustomer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 13, 54, 40, 999, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 13, 54, 40, 999, DateTimeKind.Utc).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 13, 54, 40, 999, DateTimeKind.Utc).AddTicks(1994));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 29, 13, 54, 40, 999, DateTimeKind.Utc).AddTicks(1996));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "DiscountsCustomer");

            migrationBuilder.CreateTable(
                name: "LoyalityPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RentalCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyalityPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyalityPrograms_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityPrograms_CustomerId",
                table: "LoyalityPrograms",
                column: "CustomerId");
        }
    }
}
