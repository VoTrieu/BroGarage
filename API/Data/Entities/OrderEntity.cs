using BroGarage.API.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Orders")]
    [Index(nameof(CreatedDate))]
    public class OrderEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int? CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual CarEntity Car { get; set; } = null!;

        /// <summary>
        /// Status:
        /// 1: Price Quotation
        /// 2: In progress
        /// 3: Ready for delivery
        /// 4: Completed
        /// </summary>
        public int? StatusId { get; set; } = OrderStatusEnum.PRICE_QUOTATION_1;

        [ForeignKey(nameof(StatusId))]
        public virtual OrderStatusEntity Status { get; set; } = null!;

        public int? TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual OrderTypeEntity OrderType { get; set; } = null!;

        public int? TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual TemplateEntity? Template { get; set; }

        [MaxLength(30)]
        public string OrderCode { get; set; } = "";

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateIn { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOutEstimated { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOutActual { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ODOCurrent { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ODONext { get; set; }

        [MaxLength(30)]
        public string ODOUnit { get; set; } = "";

        [Column(TypeName = "date")]
        public DateTime ExpiredInDate { get; set; }

        /// <summary>
        /// Có xuất hóa đơn hay không
        /// </summary>
        public bool IsInvoice { get; set; }

        /// <summary>
        /// Tiền tạm ứng
        /// </summary>
        [Range(0, long.MaxValue)]
        public long AdvancePayment { get; set; }

        /// <summary>
        /// Hình thức thanh toán:
        /// CASH    : Tiền mặt
        /// TRANSFER: Chuyển khoản
        /// </summary>
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = PaymentMethodEnum.CASH;

        /// <summary>
        /// Chuẩn đoán tình trạng khi vào xưởng
        /// </summary>
        [StringLength(500)]
        public string Diagnosis { get; set; } = "";

        [StringLength(500)]
        public string CustomerNote { get; set; } = "";

        [StringLength(500)]
        public string InternalNote { get; set; } = "";

        [Column(TypeName = "decimal(3, 2)")]
        public decimal VAT { get; set; }

        [Range(0, long.MaxValue)]
        public long Discount { get; set; }

        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; } = null!;
    }
}
