using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds;

public class CustomerSeed : BaseSeed
{
    public static List<CustomerEntity> Data => new()
    {
        new CustomerEntity
        {
            CustomerId = 1,
            TypeId = 1,
            FullName = "Nguyen Van An",
            PhoneNumber = "0901000001",
            Address = "12 Nguyen Trai, District 1, TP.HCM",
            Email = "an.nguyen@example.com",
            Note = "Personal customer",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CustomerEntity
        {
            CustomerId = 2,
            TypeId = 1,
            FullName = "Tran Thi Binh",
            PhoneNumber = "0901000002",
            Address = "45 Le Loi, District 3, TP.HCM",
            Email = "binh.tran@example.com",
            Note = "Regular maintenance is necessary.",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new CustomerEntity
        {
            CustomerId = 3,
            TypeId = 2,
            FullName = "Minh Long Inc",
            PhoneNumber = "0901000003",
            Representative = "Le Minh",
            TaxCode = "0312345678",
            Address = "88 Dien Bien Phu, Binh Thanh, TP.HCM",
            Email = "service@minhlong.example.com",
            Note = "Bussiness customer",
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
