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
            UnitName = "Bo",
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
            UnitName = "Lan",
            UnitPrice = 300000,
            Quantity = 0,
            Remark = "Service",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
