using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IUserRepository
    {
        bool GetByUserId(string email);
        void AddUser(User user);
    }
}
