using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds
{
    public class OrderStatusSeed : BaseSeed
    {
        public static List<OrderStatusEntity> Data => new()
        {
            new OrderStatusEntity()
            {
                StatusId = 1,
                StatusName = "Price Quotation",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 2,
                StatusName = "In progress",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 3,
                StatusName = "Ready for delivery",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 4,
                StatusName = "Completed",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            }
        };
    }
}
