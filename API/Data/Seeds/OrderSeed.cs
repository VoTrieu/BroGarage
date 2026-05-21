using BroGarage.API.Data.Entities;
using BroGarage.API.Shared.Enums;

namespace BroGarage.API.Data.Seeds;

public class OrderSeed : BaseSeed
{
    public static List<OrderEntity> Data => new()
    {
        new OrderEntity
        {
            OrderId = 1,
            CarId = 1,
            StatusId = OrderStatusEnum.COMPLETED_4,
            TypeId = 1,
            TemplateId = 1,
            OrderCode = "BG-000001",
            OrderDate = new DateTime(2026, 5, 1),
            DateIn = new DateTime(2026, 5, 1),
            DateOutEstimated = new DateTime(2026, 5, 2),
            DateOutActual = new DateTime(2026, 5, 2),
            ODOCurrent = 25000,
            ODONext = 30000,
            ODOUnit = "km",
            ExpiredInDate = new DateTime(2026, 11, 1),
            IsInvoice = true,
            AdvancePayment = 300000,
            PaymentMethod = PaymentMethodEnum.CASH,
            Diagnosis = "Bao duong dinh ky",
            CustomerNote = "Kiem tra tieng on khi chay cham",
            InternalNote = "Da thay dau va loc dau",
            VAT = 0.10m,
            Discount = 50000,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        },
        new OrderEntity
        {
            OrderId = 2,
            CarId = 2,
            StatusId = OrderStatusEnum.REPAIRING_2,
            TypeId = 2,
            TemplateId = null,
            OrderCode = "BG-000002",
            OrderDate = new DateTime(2026, 5, 15),
            DateIn = new DateTime(2026, 5, 15),
            DateOutEstimated = new DateTime(2026, 5, 17),
            DateOutActual = null,
            ODOCurrent = 42000,
            ODONext = 47000,
            ODOUnit = "km",
            ExpiredInDate = new DateTime(2026, 11, 15),
            IsInvoice = false,
            AdvancePayment = 500000,
            PaymentMethod = PaymentMethodEnum.TRANSFER,
            Diagnosis = "Phanh truoc phat tieng keu",
            CustomerNote = "Can bao gia truoc khi thay phu tung",
            InternalNote = "Dang kiem tra he thong phanh",
            VAT = 0.10m,
            Discount = 0,
            CreatedDate = CreatedDate,
            CreatedTime = CreatedTime,
            CreatedTimeStamp = CreatedTimeStamp,
            CreatedUserId = CreatedUserId
        }
    };
}
