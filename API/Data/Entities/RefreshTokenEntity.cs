using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("RefreshTokens")]
    [Index(nameof(RefreshToken), IsUnique = true)]
    [Index(nameof(ExpirationInDate))]
    public class RefreshTokenEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefreshTokenId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string? RefreshToken { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? ExpirationInDate { get; set; }
    }
}