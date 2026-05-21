using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class OrderDetailSeed : BaseSeed
{
    public static List<OrderDetailEntity> Data => new()
    {
        new OrderDetailEntity
        {
            OrderDetailId = 1,
            OrderId = 1,
            ProductId = 1,
            UnitPrice = 180000,
            Quantity = 4,
            Comment = "Thay dau dong co",
            IsHideProduct = false,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new OrderDetailEntity
        {
            OrderDetailId = 2,
            OrderId = 1,
            ProductId = 2,
            UnitPrice = 120000,
            Quantity = 1,
            Comment = "Thay loc dau",
            IsHideProduct = false,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new OrderDetailEntity
        {
            OrderDetailId = 3,
            OrderId = 2,
            ProductId = 3,
            UnitPrice = 850000,
            Quantity = 1,
            Comment = "Du kien thay ma phanh truoc",
            IsHideProduct = false,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new OrderDetailEntity
        {
            OrderDetailId = 4,
            OrderId = 2,
            ProductId = 4,
            UnitPrice = 300000,
            Quantity = 1,
            Comment = "Cong kiem tra tong quat",
            IsHideProduct = false,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
