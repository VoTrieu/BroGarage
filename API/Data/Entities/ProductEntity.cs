using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Products")]
    [Index(nameof(ProductCode), IsUnique = true)]
    [Index(nameof(ProductName), IsUnique = true)]
    public class ProductEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string? ProductCode { get; set; }

        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [StringLength(500)]
        public string Remark { get; set; } = "";

        [StringLength(50)]
        public string UnitName { get; set; } = "";

        [Range(0, long.MaxValue)]
        public long UnitPrice { get; set; }

        [Range(int.MinValue, int.MaxValue)]
        public int Quantity { get; set; }

        [MaxLength(500)]
        public string AvatarUrl { get; set; } = "";

        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; } = null!;

        public virtual ICollection<TemplateDetailEntity> TemplateDetails { get; set; } = null!;
    }
}
