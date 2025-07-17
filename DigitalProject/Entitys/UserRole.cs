using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalProject.Entitys
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User users { get; set; }
        public virtual Role roles { get; set; }
    }
}
