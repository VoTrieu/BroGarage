using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BroGarage.API.Data.Entities
{
    [Table("Users")]
    [Index(nameof(UserName))]
    public class UserEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string? UserName { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(70)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(128)]
        public string? Salt { get; set; }

        [Required]
        [StringLength(200)]
        public string? PasswordHash { get; set; }

        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = null!;
    }
}
