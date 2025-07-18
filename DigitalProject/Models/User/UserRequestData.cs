﻿namespace DigitalProject.Models.User
{
    public class UserRequestData
    {
       
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
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
