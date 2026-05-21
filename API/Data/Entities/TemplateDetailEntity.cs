using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("TemplateDetails")]
    public class TemplateDetailEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateDetailId { get; set; }

        public int? TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual TemplateEntity Template { get; set; } = null!;

        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductEntity Product { get; set; } = null!;

        [Range(0, 1_000)]
        public int Quantity { get; set; }
    }
}
