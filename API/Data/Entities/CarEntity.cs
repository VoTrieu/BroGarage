using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Cars")]
    public class CarEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        public int? CarTypeId { get; set; }

        [ForeignKey(nameof(CarTypeId))]
        public virtual CarTypeEntity CarType { get; set; } = null!;

        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerEntity Customer { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string? LicensePlate { get; set; }

        [Range(0, 3_000)]
        public int YearOfManufacture { get; set; }

        [StringLength(50)]
        public string VIN { get; set; } = "";

        [StringLength(500)]
        public string AvatarUrl { get; set; } = "";

        public virtual ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
