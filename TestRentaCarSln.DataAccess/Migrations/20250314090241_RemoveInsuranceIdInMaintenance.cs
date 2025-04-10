using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class RemoveInsuranceIdInMaintenance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Insurances_InsuranceId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_InsuranceId",
                table: "Maintenances");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ee1a71f-0b60-4e50-8f65-c764f92bdb96");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea2ea4ba-f9ba-45cb-8cd9-6d9ee8cc6caa");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Maintenances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06749cd0-59a4-4fc7-b1e7-d2ea72608775", "46127bf7-6316-456d-8cb5-2a8bc1e8990f", "Customer", null },
                    { "6da7361e-16b3-4712-a464-d42e3762032e", "d266041c-099c-4360-a0e5-4e7fe1ccd3b6", "Admin", null }
                });

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 9, 2, 40, 812, DateTimeKind.Utc).AddTicks(4367));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 9, 2, 40, 812, DateTimeKind.Utc).AddTicks(4372));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 9, 2, 40, 812, DateTimeKind.Utc).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 9, 2, 40, 812, DateTimeKind.Utc).AddTicks(4374));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06749cd0-59a4-4fc7-b1e7-d2ea72608775");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6da7361e-16b3-4712-a464-d42e3762032e");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "Maintenances",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ee1a71f-0b60-4e50-8f65-c764f92bdb96", "f999f434-6a80-4cc9-9a43-6659d15eec73", "Admin", null },
                    { "ea2ea4ba-f9ba-45cb-8cd9-6d9ee8cc6caa", "c81ae00b-f56d-4f54-9899-6d5620439a21", "Customer", null }
                });

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 8, 48, 49, 982, DateTimeKind.Utc).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 8, 48, 49, 982, DateTimeKind.Utc).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 8, 48, 49, 982, DateTimeKind.Utc).AddTicks(6974));

            migrationBuilder.UpdateData(
                table: "CarCatagories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 14, 8, 48, 49, 982, DateTimeKind.Utc).AddTicks(6976));

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_InsuranceId",
                table: "Maintenances",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Insurances_InsuranceId",
                table: "Maintenances",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");
        }
    }
}
