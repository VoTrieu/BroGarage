using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class TemplateDetailSeed : BaseSeed
{
    public static List<TemplateDetailEntity> Data => new()
    {
        new TemplateDetailEntity
        {
            TemplateDetailId = 1,
            TemplateId = 1,
            ProductId = 1,
            Quantity = 4,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new TemplateDetailEntity
        {
            TemplateDetailId = 2,
            TemplateId = 1,
            ProductId = 2,
            Quantity = 1,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new TemplateDetailEntity
        {
            TemplateDetailId = 3,
            TemplateId = 2,
            ProductId = 1,
            Quantity = 4,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new TemplateDetailEntity
        {
            TemplateDetailId = 4,
            TemplateId = 2,
            ProductId = 4,
            Quantity = 1,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
