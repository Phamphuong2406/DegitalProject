using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Interface;

namespace DigitalProject.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<User> GetListUser()
        {
            return _context.users.ToList();
        }
        public void AddUser(User model)
        {
            _context.users.Add(model);
            _context.SaveChanges();
        }
        public User FindByEmail(string email)
        {
            return _context.users.FirstOrDefault(x => x.Email == email);

        }
        public User FindById(int id)
        {
            return _context.users.FirstOrDefault(x => x.UserId == id);

        }
        public void EditUser(User model)
        {
            _context.users.Update(model);
           _context.SaveChanges();
        }
        public void UpdateRefreshToken(int idUser, string refreshToken, DateTime refreshTokenExprired)
        {
            var userUpdate = FindById(idUser);
            userUpdate.RefreshToken = refreshToken;
            userUpdate.RefreshTokenExpired = refreshTokenExprired;
            _context.SaveChanges();
        }
        public void DeleteUser(User model)
        {
            _context.users.Remove(model);
            _context.SaveChanges();
        }
        public PagingModel<UserDTO> GetUserByKey(string? key, bool isActive, int pageNumber, int pageSize)
        {
            var query = _context.users.Where(x => x.IsActive == isActive);

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.UserName.Contains(key) || x.Email.Contains(key));
            }
            var totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var data = _mapper.Map<List<UserDTO>>(pagedData);
            return new PagingModel<UserDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data,
                TotalRecords = totalRecords
            };
        }
     

    }
}
