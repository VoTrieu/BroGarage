using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds
{
    public class OrderTypeSeed : BaseSeed
    {
        public static List<OrderTypeEntity> Data => new()
        {
            new OrderTypeEntity()
            {
                TypeId = 1,
                TypeName = "Bảo dưỡng",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new OrderTypeEntity()
            {
                TypeId = 2,
                TypeName = "Sửa chữa",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            }
        };
    }
}
