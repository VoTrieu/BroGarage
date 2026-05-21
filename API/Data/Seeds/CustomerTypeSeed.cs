using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds
{
    public class CustomerTypeSeed : BaseSeed
    {
        public static List<CustomerTypeEntity> Data => new()
        {
            new CustomerTypeEntity()
            {
                TypeId = 1,
                TypeName = "Cá nhân",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            },
            new CustomerTypeEntity()
            {
                TypeId = 2,
                TypeName = "Doanh nghiệp",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp,
                CreatedUserId = CreatedUserId
            }
        };
    }
}
