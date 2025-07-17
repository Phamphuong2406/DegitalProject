using DigitalProject.Entitys;
using DigitalProject.Models.User;

namespace DigitalProject.Services.Interface
{
    public interface IUserService
    {
        string  CreateUser(UserRequestData Dto);
        User getByUserId(int userId);
        bool EditUser(UserRequestData Dto, int userId);
    }
}
