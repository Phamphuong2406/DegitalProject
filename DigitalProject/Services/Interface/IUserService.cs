using DigitalProject.Entitys;
using DigitalProject.Models.User;

namespace DigitalProject.Services.Interface
{
    public interface IUserService
    {
        List<User> GetListUser();
        string  CreateUser(UserRequestData Dto);
        User getByUserId(int userId);
        bool EditUser(UserRequestData Dto, int userId);
        bool DeleteUser(int userId);
        List<User> GetByKeyword(string? key, bool isActive);
        User LoginUser(AccountLoginRequestData data);
        bool AccountUpdateRefreshToken(AccountUpdateRefeshTokenRequestData tokenRequestData);
    }
}
