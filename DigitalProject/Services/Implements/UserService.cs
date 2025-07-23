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

        public User GetUserById(int userId)
        {
            try
            {
                return _userRepo.FindById(userId);
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
                var result = _userRepo.GetListUser();
                return _mapper.Map<List<UserDTO>>(result);//map entity => DTO
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserDTO GetByUserId(int userId)
        {
            try
            {
                var user = _userRepo.FindById(userId);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool CreateUser(UserRequestData modelDto)
        {
            try
            {
                var userExist = _userRepo.FindByEmail(modelDto.Email);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(modelDto.Password);
                var user = _mapper.Map<User>(modelDto);
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
        public void EditUser(UserRequestData Dto, int userId)
        {
            try
            {
                var user = _userRepo.FindById(userId);
                user.UserName = Dto.UserName;
                user.Email = Dto.Email;
                user.PhoneNumber = Dto.PhoneNumber;
                user.note = Dto.note;
                user.IsActive = Dto.IsActive;
                _userRepo.EditUser(user);
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
                var user = _userRepo.FindById(userId);
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
                var user_db = _userRepo.FindByEmail(data.Email);
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(data.Password, user_db.HashedPassword);
                return _mapper.Map<ClaimCreationData>(user_db);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AccountUpdateRefreshToken(AccountUpdateRefeshTokenRequestData tokenRequestData)
        {
            //nếu đăng nhập thành công thì taọ refeshtoken
            try
            {
                var user = _userRepo.FindById(tokenRequestData.Id);
                user.RefreshToken = tokenRequestData.RefreshToken;
                user.RefreshTokenExpired = tokenRequestData.RefreshTokenExprired;
                _userRepo.UpdateRefreshToken(tokenRequestData.Id, tokenRequestData.RefreshToken, tokenRequestData.RefreshTokenExprired);
            }
            catch (Exception)
            { throw; }
        }


    }
}
