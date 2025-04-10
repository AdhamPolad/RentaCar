using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class AddRoleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06b6fdf8-4752-4d39-8dfe-d118028fb5b4", "955d3e09-402e-4a31-b22f-903a2f10c027", "Admin", null },
                    { "7dfdcab9-4284-424c-99d5-d85ed0464298", "63c2f7a2-a3e4-47cf-b453-29fbda69274a", "Customer", null }
                });

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 18, 21, 2, 17, DateTimeKind.Utc).AddTicks(7131));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 18, 21, 2, 17, DateTimeKind.Utc).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 18, 21, 2, 17, DateTimeKind.Utc).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 18, 21, 2, 17, DateTimeKind.Utc).AddTicks(7137));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06b6fdf8-4752-4d39-8dfe-d118028fb5b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7dfdcab9-4284-424c-99d5-d85ed0464298");

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 17, 59, 51, 869, DateTimeKind.Utc).AddTicks(1869));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 17, 59, 51, 869, DateTimeKind.Utc).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 17, 59, 51, 869, DateTimeKind.Utc).AddTicks(1876));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 10, 17, 59, 51, 869, DateTimeKind.Utc).AddTicks(1877));
        }
    }
}
