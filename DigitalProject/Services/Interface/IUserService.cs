using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.User;

namespace DigitalProject.Services.Interface
{
    public interface IUserService
    {
        List<UserDTO> GetListUser();
        bool  CreateUser(UserRequestData dto);
        UserDTO GetByUserId(int userId);
        void EditUser(UserRequestData Dto, int userId);
        void DeleteUser(int userId);
        PagingModel<UserDTO> GetByKeyword(string? key, bool isActive, int pageNumber, int pageSize);
        ClaimCreationData LoginUser(AccountLoginRequestData data);
        void AccountUpdateRefreshToken(AccountUpdateRefeshTokenRequestData tokenRequestData);
    }
}
