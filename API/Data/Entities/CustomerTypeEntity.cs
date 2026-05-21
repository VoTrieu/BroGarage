using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("CustomerTypes")]
    public class CustomerTypeEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string? TypeName { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; } = null!;
    }
}
