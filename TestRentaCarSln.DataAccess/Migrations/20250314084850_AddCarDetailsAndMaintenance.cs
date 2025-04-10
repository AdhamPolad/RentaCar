using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRentaCarSln.DataAccess.Migrations
{
    public partial class AddCarDetailsAndMaintenance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Branch_BranchId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engines_EnginId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Insurance_InsuranceId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Cars_CarId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EnginId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insurance",
                table: "Insurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06b6fdf8-4752-4d39-8dfe-d118028fb5b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7dfdcab9-4284-424c-99d5-d85ed0464298");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EnginId",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Insurance",
                newName: "Insurances");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branches");

            migrationBuilder.RenameIndex(
                name: "IX_Review_CarId",
                table: "Reviews",
                newName: "IX_Reviews_CarId");

            migrationBuilder.AddColumn<int>(
                name: "CarDetailId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineId = table.Column<int>(type: "int", nullable: false),
                    Transmision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    DoorsCount = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarDetails_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TotalCost = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    InsuranceCoverage = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: true),
                    InsuranceId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenances_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id");
                });

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
                name: "IX_Cars_CarDetailId",
                table: "Cars",
                column: "CarDetailId",
                unique: true,
                filter: "[CarDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_EngineId",
                table: "CarDetails",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_InsuranceId",
                table: "Maintenances",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_RentalId",
                table: "Maintenances",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarDetails_CarDetailId",
                table: "Cars",
                column: "CarDetailId",
                principalTable: "CarDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Insurances_InsuranceId",
                table: "Cars",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cars_CarId",
                table: "Reviews",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Branches_BranchId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarDetails_CarDetailId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Insurances_InsuranceId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cars_CarId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "CarDetails");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarDetailId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ee1a71f-0b60-4e50-8f65-c764f92bdb96");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea2ea4ba-f9ba-45cb-8cd9-6d9ee8cc6caa");

            migrationBuilder.DropColumn(
                name: "CarDetailId",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Insurances",
                newName: "Insurance");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branch");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CarId",
                table: "Review",
                newName: "IX_Review_CarId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnginId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Review",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insurance",
                table: "Insurance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EnginId",
                table: "Cars",
                column: "EnginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Branch_BranchId",
                table: "Cars",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engines_EnginId",
                table: "Cars",
                column: "EnginId",
                principalTable: "Engines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Insurance_InsuranceId",
                table: "Cars",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Cars_CarId",
                table: "Review",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
