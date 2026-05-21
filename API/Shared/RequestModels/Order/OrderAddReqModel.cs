using BroGarage.API.Shared.Enums;
using BroGarage.API.Shared.RequestModels.OrderDetail;
using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Order
{
    public class OrderAddReqModel : BaseReqModel
    {
        public int CarId { get; set; }

        public int StatusId { get; set; }

        public int TypeId { get; set; }

        public int? TemplateId { get; set; } = null;

        [MaxLength(30, ErrorMessage = MAX_STR)]
        public string OrderCode { get; set; } = "";

        [MaxLength(10, ErrorMessage = MAX_STR)]
        public string OrderDate { get; set; } = "";

        [MaxLength(10, ErrorMessage = MAX_STR)]
        public string DateIn { get; set; } = "";

        [MaxLength(10, ErrorMessage = MAX_STR)]
        public string DateOutEstimated { get; set; } = "";

        public decimal ODOCurrent { get; set; }

        public decimal ODONext { get; set; }

        [MaxLength(30, ErrorMessage = MAX_STR)]
        public string ODOUnit { get; set; } = "";

        [MaxLength(10, ErrorMessage = MAX_STR)]
        public string ExpiredInDate { get; set; } = "";

        public bool IsInvoice { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = INVALID_VALUE)]
        public long AdvancePayment { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = PaymentMethodEnum.CASH;

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string Diagnosis { get; set; } = "";

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string CustomerNote { get; set; } = "";

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string InternalNote { get; set; } = "";

        [Range(0, long.MaxValue)]
        public long Discount { get; set; }

        public List<OrderDetailAddReqModel> OrderDetails { get; set; } = null!;
    }
}
