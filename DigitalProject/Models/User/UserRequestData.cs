using System.ComponentModel.DataAnnotations;

namespace DigitalProject.Models.User
{
    public class UserRequestData
    {

        [Required(ErrorMessage = "Vui lòng điền tên")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên đầy đủ")]
        [StringLength(20, ErrorMessage = "Tên không được vượt quá 20 ký tự.")]
        public string FullName { get; set; }
        public string? Password { get; set; }
        [Required(ErrorMessage = "Vui lòng điền email")]
        [EmailAddress(ErrorMessage = "Vui lòng điền đúng định dạng email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số điện thoại")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string? note { get; set; }
    }
    public class AccountLoginRequestData
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
