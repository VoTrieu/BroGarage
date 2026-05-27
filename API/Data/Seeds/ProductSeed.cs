using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class ProductSeed : BaseSeed
{
    public static List<ProductEntity> Data => new()
    {
        new ProductEntity
        {
            ProductId = 1,
            ProductCode = "OIL-5W30",
            ProductName = "5W-30 engine oil",
            UnitName = "Lit",
            UnitPrice = 180000,
            Quantity = 60,
            Remark = "Synthetic oil",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 2,
            ProductCode = "FILTER-OIL",
            ProductName = "Engine oil filter",
            UnitName = "Piece",
            UnitPrice = 120000,
            Quantity = 40,
            Remark = "Replacement included during maintenance.",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 3,
            ProductCode = "BRAKE-PAD",
            ProductName = "Front brake pads",
            UnitName = "Set",
            UnitPrice = 850000,
            Quantity = 20,
            Remark = "Brake parts",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 4,
            ProductCode = "LABOR-GEN",
            ProductName = "General inspection",
            UnitName = "Time",
            UnitPrice = 300000,
            Quantity = 0,
            Remark = "Service",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
        ,
        new ProductEntity
        {
            ProductId = 5,
            ProductCode = "OIL-10W40",
            ProductName = "10W-40 engine oil",
            UnitName = "Lit",
            UnitPrice = 150000,
            Quantity = 80,
            Remark = "Semi-synthetic",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 6,
            ProductCode = "AIR-FILTER",
            ProductName = "Air filter",
            UnitName = "Piece",
            UnitPrice = 90000,
            Quantity = 120,
            Remark = "Standard air filter",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 7,
            ProductCode = "SPARK-PLG",
            ProductName = "Spark plug",
            UnitName = "Piece",
            UnitPrice = 40000,
            Quantity = 300,
            Remark = "High performance",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 8,
            ProductCode = "WIPER-SET",
            ProductName = "Windshield wiper set",
            UnitName = "Set",
            UnitPrice = 220000,
            Quantity = 50,
            Remark = "All-season",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 9,
            ProductCode = "BAT-55AH",
            ProductName = "Car battery 55Ah",
            UnitName = "Piece",
            UnitPrice = 1200000,
            Quantity = 25,
            Remark = "Maintenance-free",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 10,
            ProductCode = "OIL-FLTR-SET",
            ProductName = "Oil filter set",
            UnitName = "Set",
            UnitPrice = 250000,
            Quantity = 70,
            Remark = "Includes gasket",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 11,
            ProductCode = "BRAKE-DISC",
            ProductName = "Brake disc",
            UnitName = "Piece",
            UnitPrice = 450000,
            Quantity = 35,
            Remark = "Front axle",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 12,
            ProductCode = "OIL-GL-1L",
            ProductName = "Gearbox oil 1L",
            UnitName = "Bottle",
            UnitPrice = 130000,
            Quantity = 90,
            Remark = "Synthetic gearbox oil",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 13,
            ProductCode = "CLN-AC",
            ProductName = "AC cleaner",
            UnitName = "Can",
            UnitPrice = 90000,
            Quantity = 110,
            Remark = "Cleans AC system",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 14,
            ProductCode = "COOL-ANT",
            ProductName = "Coolant 1L",
            UnitName = "Bottle",
            UnitPrice = 95000,
            Quantity = 140,
            Remark = "Long-life coolant",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 15,
            ProductCode = "BELT-ALT",
            ProductName = "Alternator belt",
            UnitName = "Piece",
            UnitPrice = 60000,
            Quantity = 200,
            Remark = "Durable",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 16,
            ProductCode = "O2-SENSOR",
            ProductName = "Oxygen sensor",
            UnitName = "Piece",
            UnitPrice = 400000,
            Quantity = 40,
            Remark = "OEM quality",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 17,
            ProductCode = "FUEL-PUMP",
            ProductName = "Fuel pump",
            UnitName = "Piece",
            UnitPrice = 750000,
            Quantity = 18,
            Remark = "High flow",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 18,
            ProductCode = "TIRE-16",
            ProductName = "16-inch tire",
            UnitName = "Piece",
            UnitPrice = 900000,
            Quantity = 60,
            Remark = "All-season tire",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 19,
            ProductCode = "SHOCK-REAR",
            ProductName = "Rear shock absorber",
            UnitName = "Piece",
            UnitPrice = 650000,
            Quantity = 30,
            Remark = "Gas-filled",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 20,
            ProductCode = "FILTER-FUEL",
            ProductName = "Fuel filter",
            UnitName = "Piece",
            UnitPrice = 110000,
            Quantity = 130,
            Remark = "Micron filtration",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 21,
            ProductCode = "LUB-GREASE",
            ProductName = "Multipurpose grease",
            UnitName = "Tube",
            UnitPrice = 45000,
            Quantity = 220,
            Remark = "For bearings",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 22,
            ProductCode = "RADIATOR-CAP",
            ProductName = "Radiator cap",
            UnitName = "Piece",
            UnitPrice = 30000,
            Quantity = 150,
            Remark = "Standard",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 23,
            ProductCode = "EXH-GASKET",
            ProductName = "Exhaust gasket",
            UnitName = "Piece",
            UnitPrice = 25000,
            Quantity = 260,
            Remark = "High temp",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 24,
            ProductCode = "MIRROR-LEFT",
            ProductName = "Left side mirror",
            UnitName = "Piece",
            UnitPrice = 180000,
            Quantity = 45,
            Remark = "Heated",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
