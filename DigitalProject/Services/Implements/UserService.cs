using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace DigitalProject.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepo, ILogger<UserService> logger) { _userRepo = userRepo; _logger = logger; }
        public string CreateUser(UserRequestData ModelDto)
        {
            try
            {
                var userExist = _userRepo.GetByUserId(ModelDto.Email);
                if(userExist == true)
                {
                    return "Người dùng đã tồn tại";

                }
               // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User
                {
                    UserName = ModelDto.UserName,
                    FullName = ModelDto.Email,
                    Email = ModelDto.Email,
                    HashedPassword = ModelDto.Password,
                    PhoneNumber = ModelDto.PhoneNumber,
                    IsActive = true,
                    
                };
                _userRepo.AddUser(user);
                return "Đã thêm mới";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi hiển thị bài viết");
                throw new ApplicationException("Có lỗi xảy ra khi hiển thị bài viết");
            }
        }


    }
}
