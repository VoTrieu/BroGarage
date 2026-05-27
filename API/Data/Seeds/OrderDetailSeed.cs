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
            Comment = "Change engine oil",
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
            Comment = "Replace the oil filter",
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
            Comment = "Replace front brake pads",
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
            Comment = "General inspection",
            IsHideProduct = false,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
