using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class CarTypeSeed : BaseSeed
{
    public static List<CarTypeEntity> Data => new()
    {
        new CarTypeEntity
        {
            TypeId = 1,
            ManufacturerId = 1,
            TypeName = "Vios",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CarTypeEntity
        {
            TypeId = 2,
            ManufacturerId = 1,
            TypeName = "Camry",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CarTypeEntity
        {
            TypeId = 3,
            ManufacturerId = 2,
            TypeName = "Civic",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CarTypeEntity
        {
            TypeId = 4,
            ManufacturerId = 3,
            TypeName = "Ranger",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
