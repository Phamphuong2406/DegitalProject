namespace DigitalProject.Models.User
{
    public class UserResponseData
    {
        public int ResponseCode { get; set; } 
        public string ResponseMessage { get; set; } 
        public string token { get; set; }
        public string? refeshToken { get; set; }
    }
    public class ResponseData
    { 
     
        public string Message { get; set; } 
    }
}
