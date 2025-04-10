using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class AddCarCatagorySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarCatagories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 10, 10, 4, 40, 365, DateTimeKind.Utc).AddTicks(2342), null, false, "Sedan", null },
                    { 2, new DateTime(2025, 3, 10, 10, 4, 40, 365, DateTimeKind.Utc).AddTicks(2345), null, false, "Suv", null },
                    { 3, new DateTime(2025, 3, 10, 10, 4, 40, 365, DateTimeKind.Utc).AddTicks(2347), null, false, "Sport", null },
                    { 4, new DateTime(2025, 3, 10, 10, 4, 40, 365, DateTimeKind.Utc).AddTicks(2349), null, false, "Coupe", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
