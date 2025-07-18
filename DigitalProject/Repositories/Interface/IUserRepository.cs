using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IUserRepository
    {
        List<User> getListUser();
        User GetByEmail(string email);
        void AddUser(User user);
        User GetUserById(int id);
        bool EditUser(User model);
        void DeleteUser(User model);
        bool UpdateRefreshToken(User model);
        List<User> GetUserByKey(string? key, bool IsActive);
    }
}
