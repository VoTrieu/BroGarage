using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("OrderStatuses")]
    public class OrderStatusEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string? StatusName { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
