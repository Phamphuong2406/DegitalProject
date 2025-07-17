namespace DigitalProject.Models.User
{
    public class UserDTO
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? HashedPassword { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RefreshToken { get; set; }
        public string? RefreshTokenExpired { get; set; }
        public bool? IsActive { get; set; }
        public string? note { get; set; }
    }
    public class ResponseData
    { 
        public string Message { get; set; } 
    }
}
