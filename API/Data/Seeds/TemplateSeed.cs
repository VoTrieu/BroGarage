using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class TemplateSeed : BaseSeed
{
    public static List<TemplateEntity> Data => new()
    {
        new TemplateEntity
        {
            TemplateId = 1,
            CarTypeId = 1,
            YearOfManufactureFrom = 2018,
            YearOfManufactureTo = 2023,
            Note = "Bao duong Toyota Vios moi 5.000 km",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new TemplateEntity
        {
            TemplateId = 2,
            CarTypeId = 3,
            YearOfManufactureFrom = 2017,
            YearOfManufactureTo = 2022,
            Note = "Bao duong Honda Civic dinh ky",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
