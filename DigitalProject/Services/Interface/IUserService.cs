using DigitalProject.Entitys;
using DigitalProject.Models.User;

namespace DigitalProject.Services.Interface
{
    public interface IUserService
    {
        List<UserDTO> GetListUser();
        string  CreateUser(UserRequestData Dto);
        UserDTO getByUserId(int userId);
        bool EditUser(UserRequestData Dto, int userId);
        bool DeleteUser(int userId);
        List<User> GetByKeyword(string? key, bool isActive);
        User LoginUser(AccountLoginRequestData data);
        bool AccountUpdateRefreshToken(AccountUpdateRefeshTokenRequestData tokenRequestData);
    }
}
