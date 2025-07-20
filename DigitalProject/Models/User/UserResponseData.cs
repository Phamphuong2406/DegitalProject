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
        public DateTime? RefreshTokenExpired { get; set; }
        public bool? IsActive { get; set; }
        public string? note { get; set; }
    }
    public class ResponseData
    { 
        public string Message { get; set; } 
    }
    public class DataReturnedAfterLogin
    {
        public int ResponseCode { get; set; } //mã lỗi
        public string ResponseMessage { get; set; } // thông báo lỗi
        public string token { get; set; }
        public string? refeshToken { get; set; }
    }
    public class AccountUpdateRefeshTokenRequestData 
    {
        public int Id { get; set; }
        public string? RefreshToken { get; set; } = null!;
        public DateTime? RefreshTokenExprired { get; set; }
    }
}
