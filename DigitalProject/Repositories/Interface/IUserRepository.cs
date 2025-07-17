using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IUserRepository
    {
        bool GetByEmail(string email);
        void AddUser(User user);
        User GetUserById(int id);
        bool EditUser(User model);
    }
}
