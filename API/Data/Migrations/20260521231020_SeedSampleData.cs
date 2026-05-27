using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BroGarage.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "AvatarUrl", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "Email", "FullName", "Note", "PhoneNumber", "Representative", "TaxCode", "TypeId", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, "12 Nguyen Trai, District 1, TP.HCM", "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "an.nguyen@example.com", "Nguyen Van An", "Personal customer", "0901000001", "", "", 1, null, 0 },
                    { 2, "45 Le Loi, District 3, TP.HCM", "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "binh.tran@example.com", "Tran Thi Binh", "Regular maintenance is necessary.", "0901000002", "", "", 1, null, 0 },
                    { 3, "88 Dien Bien Phu, Binh Thanh, TP.HCM", "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "service@minhlong.example.com", "Minh Long Inc", "Bussiness customer", "0901000003", "Le Minh", "0312345678", 2, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "ManufacturerId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "ManufacturerName", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Toyota", null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Honda", null, 0 },
                    { 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Ford", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvatarUrl", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "ProductCode", "ProductName", "Quantity", "Remark", "UnitName", "UnitPrice", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "OIL-5W30", "5W-30 engine oil", 60, "Synthetic oil", "Lit", 180000L, null, 0 },
                    { 2, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "FILTER-OIL", "Engine oil filter", 40, "Replacement included during maintenance.", "Piece", 120000L, null, 0 },
                    { 3, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "BRAKE-PAD", "Front brake pads", 20, "Brake parts", "Bo", 850000L, null, 0 },
                    { 4, "", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "LABOR-GEN", "General inspection", 0, "Service", "Lan", 300000L, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "TypeId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "ManufacturerId", "TypeName", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 1, "Vios", null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 1, "Camry", null, 0 },
                    { 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 2, "Civic", null, 0 },
                    { 4, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 3, "Ranger", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "AvatarUrl", "CarTypeId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "CustomerId", "LicensePlate", "UpdatedDateTime", "UpdatedUserId", "VIN", "YearOfManufacture" },
                values: new object[,]
                {
                    { 1, "", 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 1, "51A-12345", null, 0, "JTDBR32E720012345", 2020 },
                    { 2, "", 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 2, "51G-67890", null, 0, "2HGFC2F59KH123456", 2019 },
                    { 3, "", 4, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 3, "51C-24680", null, 0, "MPBUMFF60NX123456", 2022 }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "TemplateId", "CarTypeId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "Note", "UpdatedDateTime", "UpdatedUserId", "YearOfManufactureFrom", "YearOfManufactureTo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "5,000 km Maintenance for New Toyota Vios", null, 0, 2018, 2023 },
                    { 2, 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Regular maintenance for Honda Civic", null, 0, 2017, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AdvancePayment", "CarId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "CustomerNote", "DateIn", "DateOutActual", "DateOutEstimated", "Diagnosis", "Discount", "ExpiredInDate", "InternalNote", "IsInvoice", "ODOCurrent", "ODONext", "ODOUnit", "OrderCode", "OrderDate", "PaymentMethod", "StatusId", "TemplateId", "TypeId", "UpdatedDateTime", "UpdatedUserId", "VAT" },
                values: new object[,]
                {
                    { 1, 300000L, 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Check for noise when running at low speed.", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Regular maintenance", 50000L, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The oil and oil filter have been changed", true, 25000m, 30000m, "km", "BG-000001", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CASH", 4, 1, 1, null, 0, 0.10m },
                    { 2, 500000L, 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "A price quote is required before replacing any parts", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "The front brakes are making a noise", 0L, new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Checking the braking system", false, 42000m, 47000m, "km", "BG-000002", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TRANSFER", 2, null, 2, null, 0, 0.10m }
                });

            migrationBuilder.InsertData(
                table: "TemplateDetails",
                columns: new[] { "TemplateDetailId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "ProductId", "Quantity", "TemplateId", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 1, 4, 1, null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 2, 1, 1, null, 0 },
                    { 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 1, 4, 2, null, 0 },
                    { 4, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, 4, 1, 2, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "Comment", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "IsHideProduct", "OrderId", "ProductId", "Quantity", "UnitPrice", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, "Change engine oil", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, false, 1, 1, 4, 180000L, null, 0 },
                    { 2, "Replace the oil filter", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, false, 1, 2, 1, 120000L, null, 0 },
                    { 3, "Replace front brake pads", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, false, 2, 3, 1, 850000L, null, 0 },
                    { 4, "General inspection", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, false, 2, 4, 1, 300000L, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "TypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TemplateDetails",
                keyColumn: "TemplateDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TemplateDetails",
                keyColumn: "TemplateDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TemplateDetails",
                keyColumn: "TemplateDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TemplateDetails",
                keyColumn: "TemplateDetailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "TypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "TemplateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "TemplateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "TypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "TypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 2);
        }
    }
}
