using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class CarSeed : BaseSeed
{
    public static List<CarEntity> Data => new()
    {
        new CarEntity
        {
            CarId = 1,
            CarTypeId = 1,
            CustomerId = 1,
            LicensePlate = "51A-12345",
            YearOfManufacture = 2020,
            VIN = "JTDBR32E720012345",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CarEntity
        {
            CarId = 2,
            CarTypeId = 3,
            CustomerId = 2,
            LicensePlate = "51G-67890",
            YearOfManufacture = 2019,
            VIN = "2HGFC2F59KH123456",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CarEntity
        {
            CarId = 3,
            CarTypeId = 4,
            CustomerId = 3,
            LicensePlate = "51C-24680",
            YearOfManufacture = 2022,
            VIN = "MPBUMFF60NX123456",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
