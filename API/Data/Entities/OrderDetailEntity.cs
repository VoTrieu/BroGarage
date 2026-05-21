using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("OrderDetails")]
    public class OrderDetailEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual OrderEntity Order { get; set; } = null!;

        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductEntity Product { get; set; } = null!;

        [Range(0, 10_000_000_000)]
        public long UnitPrice { get; set; }

        [Range(0, 1_000)]
        public int Quantity { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; } = "";

        public bool IsHideProduct { get; set; }
    }
}
