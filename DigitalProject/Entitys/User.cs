using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalProject.Entitys
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpired { get; set; }
        public bool IsActive { get; set; }
        public string? note { get; set; }
        public ICollection<UserRole> userRoles { get; set; }
        public ICollection<Project> projects { get; set; }
        public ICollection<Gallery> galleries { get; set; }
    }
}
