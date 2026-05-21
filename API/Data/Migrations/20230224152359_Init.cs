using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BroGarage.API.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "TypeId");
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_CarTypes_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExpirationInDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    YearOfManufactureFrom = table.Column<int>(type: "int", nullable: false),
                    YearOfManufactureTo = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateId);
                    table.ForeignKey(
                        name: "FK_Templates_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    OrderCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    DateIn = table.Column<DateTime>(type: "date", nullable: false),
                    DateOutEstimated = table.Column<DateTime>(type: "date", nullable: false),
                    DateOutActual = table.Column<DateTime>(type: "date", nullable: true),
                    ODOCurrent = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ODONext = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ODOUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExpiredInDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsInvoice = table.Column<bool>(type: "bit", nullable: false),
                    AdvancePayment = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CustomerNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InternalNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_Orders_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "TemplateId");
                });

            migrationBuilder.CreateTable(
                name: "TemplateDetails",
                columns: table => new
                {
                    TemplateDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateDetails", x => x.TemplateDetailId);
                    table.ForeignKey(
                        name: "FK_TemplateDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateDetails_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsHideProduct = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "TypeId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "TypeName", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Cá nhân", null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Doanh nghiệp", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "StatusId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "StatusName", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Báo giá", null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Đang sửa chữa", null, 0 },
                    { 3, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Chờ giao xe", null, 0 },
                    { 4, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Hoàn thành", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "TypeId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "TypeName", "UpdatedDateTime", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Bảo dưỡng", null, 0 },
                    { 2, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 1, "Sửa chữa", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "CreatedTime", "CreatedTimeStamp", "CreatedUserId", "FullName", "PasswordHash", "Salt", "UpdatedDateTime", "UpdatedUserId", "UserName" },
                values: new object[] { 1, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 14, 0, 0), 1666210440L, 0, "Quản trị viên", "ea871309f6e9a5490bd909aaf2f80f57a7dca87397af1d454c95652fc4201deb", "ztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw", null, 0, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes_ManufacturerId",
                table: "CarTypes",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes_TypeName",
                table: "CarTypes",
                column: "TypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TypeId",
                table: "Customers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ManufacturerName",
                table: "Manufacturers",
                column: "ManufacturerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedDate",
                table: "Orders",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TemplateId",
                table: "Orders",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypeId",
                table: "Orders",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCode",
                table: "Products",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ExpirationInDate",
                table: "RefreshTokens",
                column: "ExpirationInDate");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_RefreshToken",
                table: "RefreshTokens",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateDetails_ProductId",
                table: "TemplateDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateDetails_TemplateId",
                table: "TemplateDetails",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CarTypeId",
                table: "Templates",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TemplateDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
