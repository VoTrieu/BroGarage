using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Manufacturers")]
    [Index(nameof(ManufacturerName), IsUnique = true)]
    public class ManufacturerEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string? ManufacturerName { get; set; }

        public virtual ICollection<CarTypeEntity> CarTypes { get; set; } = null!;
    }
}
