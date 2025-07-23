namespace DigitalProject.Models.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
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
    public class ClaimCreationData
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
    public class AccountUpdateRefeshTokenRequestData 
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } 
        public DateTime RefreshTokenExprired { get; set; }
    }
}
