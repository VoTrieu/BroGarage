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
            ProductName = "Dau dong co 5W-30",
            UnitName = "Lit",
            UnitPrice = 180000,
            Quantity = 60,
            Remark = "Dau tong hop",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 2,
            ProductCode = "FILTER-OIL",
            ProductName = "Loc dau dong co",
            UnitName = "Cai",
            UnitPrice = 120000,
            Quantity = 40,
            Remark = "Thay kem khi bao duong",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 3,
            ProductCode = "BRAKE-PAD",
            ProductName = "Bo ma phanh truoc",
            UnitName = "Bo",
            UnitPrice = 850000,
            Quantity = 20,
            Remark = "Phu tung phanh",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ProductEntity
        {
            ProductId = 4,
            ProductCode = "LABOR-GEN",
            ProductName = "Cong kiem tra tong quat",
            UnitName = "Lan",
            UnitPrice = 300000,
            Quantity = 0,
            Remark = "Dich vu",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
