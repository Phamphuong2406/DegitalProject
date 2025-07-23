using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.User;

namespace DigitalProject.Repositories.Interface
{
    public interface IUserRepository
    {
        List<User> GetListUser();
        User FindByEmail(string email);
        void AddUser(User user);
        User FindById(int id);
        void UpdateRefreshToken(int idUser, string refreshToken, DateTime refreshTokenExprired);
        void EditUser(User model);
        void DeleteUser(User model);
        PagingModel<UserDTO> GetUserByKey(string? key, bool IsActive, int pageNumber, int pageSize);
    }
}
