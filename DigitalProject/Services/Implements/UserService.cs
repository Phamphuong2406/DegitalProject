using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Implements;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

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
        public List<User> GetListUser()
        {
            return _userRepo.getListUser();
        }
        public User getByUserId(int userId)
        {
            var user = _userRepo.GetUserById(userId);
            if (user == null)
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
                if (userExist != null)
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
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(Dto.Password);
            return _userRepo.EditUser(user);
        }
        public bool DeleteUser(int userId)
        {
            var user = _userRepo.GetUserById(userId);
            if (user == null) return false;
            _userRepo.DeleteUser(user);
            return true;
        }
        public List<User> GetByKeyword(string? key, bool isActive)
        {
            key = string.IsNullOrEmpty(key) ? "" : key.ToLower();
            var users = _userRepo.GetUserByKey(key, isActive);
            return users;

        }
        public User LoginUser(AccountLoginRequestData data)
        {
            User user = new User();
            try
            {
                var user_db = _userRepo.GetByEmail(data.Email);
                if (user_db == null) return null;
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(data.Password, user_db.HashedPassword);
                if (!isPasswordMatch)
                {
                    return null;

                }
                user.UserId = user_db.UserId;
                user.UserName = user_db.UserName;
                user.Email = user_db.Email;
                return user;


            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AccountUpdateRefreshToken(AccountUpdateRefeshTokenRequestData tokenRequestData)
        {
            //nếu đăng nhập thành công thì taọ refeshtoken
            try
            {
                var user = _userRepo.GetUserById(tokenRequestData.Id);
                if (user != null)
                {
                   
                    user.RefreshToken = tokenRequestData.RefreshToken;
                    user.RefreshTokenExpired = tokenRequestData.RefreshTokenExprired;
                  
                    _userRepo.UpdateRefreshToken(user);
                    return true;
                }
            }
            catch (Exception)
            { throw; }
            return false;
        }


    }
}
