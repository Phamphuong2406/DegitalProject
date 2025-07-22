using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.User;

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
        bool UpdateRefreshToken(int idUser, string refreshToken, string refreshTokenExprired);
        PagingModel<UserDTO> GetUserByKey(string? key, bool IsActive, int pageNumber, int pageSize);
    }
}
