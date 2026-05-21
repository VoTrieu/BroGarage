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
                StatusName = "Báo giá",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 2,
                StatusName = "Đang sửa chữa",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 3,
                StatusName = "Chờ giao xe",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderStatusEntity()
            {
                StatusId = 4,
                StatusName = "Hoàn thành",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            }
        };
    }
}
