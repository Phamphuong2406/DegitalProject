using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidatorService _validatorService;
        public UserService(IUserRepository userRepo, ILogger<UserService> logger, IMapper mapper, IValidatorService validatorService)
        {
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
            _validatorService = validatorService;
                }

        public User getUserById(int userId)
        {
            try
            {
                return _userRepo.GetUserById(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserDTO> GetListUser()
        {
            try
            {
                var result = _userRepo.getListUser();
                return _mapper.Map<List<UserDTO>>(result);//map entity => DTO
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserDTO getByUserId(int userId)
        {
            try
            {
                var user = _userRepo.GetUserById(userId);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool CreateUser(UserRequestData ModelDto)
        {
            try
            {
                var userExist = _userRepo.GetByEmail(ModelDto.Email);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(ModelDto.Password);
                var user = _mapper.Map<User>(ModelDto);
                user.HashedPassword = hashedPassword;
                user.IsActive = true;
                _userRepo.AddUser(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EditUser(UserRequestData Dto, int userId)
        {
            try
            {
                var user = _userRepo.GetUserById(userId);
                user.UserName = Dto.UserName;
                user.Email = Dto.Email;
                user.PhoneNumber = Dto.PhoneNumber;
                user.note = Dto.note;
                user.IsActive = Dto.IsActive;
                return _userRepo.EditUser(user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DeleteUser(int userId)
        {
            try
            {
                var user = _userRepo.GetUserById(userId);
                _userRepo.DeleteUser(user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public PagingModel<UserDTO> GetByKeyword(string? key, bool isActive, int pageNumber, int pageSize)
        {
            try
            {
                key = string.IsNullOrEmpty(key) ? "" : key.ToLower();
                return _userRepo.GetUserByKey(key, isActive, pageNumber, pageSize);
            }
            catch (Exception)
            {
                throw;
            }


        }
        public ClaimCreationData LoginUser(AccountLoginRequestData data)
        {
            try
            {
                var user_db = _userRepo.GetByEmail(data.Email);
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(data.Password, user_db.HashedPassword);
                return _mapper.Map<ClaimCreationData>(user_db);

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
                    _userRepo.UpdateRefreshToken(tokenRequestData.Id, tokenRequestData.RefreshToken,tokenRequestData.RefreshTokenExprired);
                    return true;
                }
                return false;
            }
            catch (Exception)
            { throw; }
        }


    }
}
