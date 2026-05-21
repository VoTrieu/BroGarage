using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("CarTypes")]
    [Index(nameof(TypeName))]
    public class CarTypeEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        public int? ManufacturerId { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        public virtual ManufacturerEntity Manufacturer { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string? TypeName { get; set; }

        public virtual ICollection<CarEntity> Cars { get; set; } = null!;

        public virtual ICollection<TemplateEntity> Templates { get; set; } = null!;
    }
}
