using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class ManufacturerSeed : BaseSeed
{
    public static List<ManufacturerEntity> Data => new()
    {
        new ManufacturerEntity
        {
            ManufacturerId = 1,
            ManufacturerName = "Toyota",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ManufacturerEntity
        {
            ManufacturerId = 2,
            ManufacturerName = "Honda",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new ManufacturerEntity
        {
            ManufacturerId = 3,
            ManufacturerName = "Ford",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
