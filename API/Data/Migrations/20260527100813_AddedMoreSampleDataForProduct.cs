using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BroGarage.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreSampleDataForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvatarUrl", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "ProductCode", "ProductName", "Quantity", "Remark", "UnitName", "UnitPrice", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 5, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "OIL-10W40", "10W-40 engine oil", 80, "Semi-synthetic", "Lit", 150000L, null, 0 },
                    { 6, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "AIR-FILTER", "Air filter", 120, "Standard air filter", "Piece", 90000L, null, 0 },
                    { 7, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "SPARK-PLG", "Spark plug", 300, "High performance", "Piece", 40000L, null, 0 },
                    { 8, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "WIPER-SET", "Windshield wiper set", 50, "All-season", "Set", 220000L, null, 0 },
                    { 9, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "BAT-55AH", "Car battery 55Ah", 25, "Maintenance-free", "Piece", 1200000L, null, 0 },
                    { 10, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "OIL-FLTR-SET", "Oil filter set", 70, "Includes gasket", "Set", 250000L, null, 0 },
                    { 11, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "BRAKE-DISC", "Brake disc", 35, "Front axle", "Piece", 450000L, null, 0 },
                    { 12, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "OIL-GL-1L", "Gearbox oil 1L", 90, "Synthetic gearbox oil", "Bottle", 130000L, null, 0 },
                    { 13, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "CLN-AC", "AC cleaner", 110, "Cleans AC system", "Can", 90000L, null, 0 },
                    { 14, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "COOL-ANT", "Coolant 1L", 140, "Long-life coolant", "Bottle", 95000L, null, 0 },
                    { 15, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "BELT-ALT", "Alternator belt", 200, "Durable", "Piece", 60000L, null, 0 },
                    { 16, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "O2-SENSOR", "Oxygen sensor", 40, "OEM quality", "Piece", 400000L, null, 0 },
                    { 17, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "FUEL-PUMP", "Fuel pump", 18, "High flow", "Piece", 750000L, null, 0 },
                    { 18, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "TIRE-16", "16-inch tire", 60, "All-season tire", "Piece", 900000L, null, 0 },
                    { 19, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "SHOCK-REAR", "Rear shock absorber", 30, "Gas-filled", "Piece", 650000L, null, 0 },
                    { 20, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "FILTER-FUEL", "Fuel filter", 130, "Micron filtration", "Piece", 110000L, null, 0 },
                    { 21, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "LUB-GREASE", "Multipurpose grease", 220, "For bearings", "Tube", 45000L, null, 0 },
                    { 22, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "RADIATOR-CAP", "Radiator cap", 150, "Standard", "Piece", 30000L, null, 0 },
                    { 23, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "EXH-GASKET", "Exhaust gasket", 260, "High temp", "Piece", 25000L, null, 0 },
                    { 24, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "MIRROR-LEFT", "Left side mirror", 45, "Heated", "Piece", 180000L, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);
        }
    }
}
