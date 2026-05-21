using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("OrderTypes")]
    public class OrderTypeEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TypeName { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
