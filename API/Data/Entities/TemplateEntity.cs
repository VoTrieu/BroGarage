using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Templates")]
    public class TemplateEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateId { get; set; }

        public int? CarTypeId { get; set; }

        [ForeignKey(nameof(CarTypeId))]
        public virtual CarTypeEntity CarType { get; set; } = null!;

        [Range(0, 3_000)]
        public int YearOfManufactureFrom { get; set; }

        [Range(0, 3_000)]
        public int YearOfManufactureTo { get; set; }

        [StringLength(500)]
        public string Note { get; set; } = "";

        public virtual ICollection<TemplateDetailEntity> TemplateDetails { get; set; } = null!;

        public virtual ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
