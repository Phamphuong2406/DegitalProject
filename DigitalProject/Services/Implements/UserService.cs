using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Implements;
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
        
        public User getUserById(int userId)
        {
            return _userRepo.GetUserById(userId);
        }
        public User getByUserId(int userId)
        {
            var user = _userRepo.GetUserById(userId);
            if(user == null)
            {
                return null;
            }
            return user;
        }
        public string CreateUser(UserRequestData ModelDto)
        {
            try
            {
                var userExist = _userRepo.GetByEmail(ModelDto.Email);
                if(userExist == true)
                {
                    return "Người dùng đã tồn tại";

                }
               string hashedPassword = BCrypt.Net.BCrypt.HashPassword(ModelDto.Password);
                var user = new User
                {
                    UserName = ModelDto.UserName,
                    FullName = ModelDto.Email,
                    Email = ModelDto.Email,
                    HashedPassword = hashedPassword,
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
        public bool EditUser(UserRequestData Dto, int userId)
        {
            var user = _userRepo.GetUserById(userId);
            if (user == null) return false;

            user.UserName = Dto.UserName;
            user.Email = Dto.Email;
            user.PhoneNumber = Dto.PhoneNumber;
            user.note = Dto.note;
            user.IsActive = Dto.IsActive;

            return  _userRepo.EditUser(user);
        }


    }
}
