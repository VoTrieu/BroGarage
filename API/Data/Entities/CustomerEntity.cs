using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Customers")]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class CustomerEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public int? TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual CustomerTypeEntity CustomerType { get; set; } = null!;

        [Required]
        [StringLength(70)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string? PhoneNumber { get; set; }

        [StringLength(70)]
        public string Representative { get; set; } = "";

        [StringLength(50)]
        public string TaxCode { get; set; } = "";

        [StringLength(500)]
        public string Address { get; set; } = "";

        [StringLength(70)]
        public string Email { get; set; } = "";

        [StringLength(500)]
        public string Note { get; set; } = "";

        [StringLength(500)]
        public string AvatarUrl { get; set; } = "";

        public virtual ICollection<CarEntity> Cars { get; set; } = null!;
    }
}
