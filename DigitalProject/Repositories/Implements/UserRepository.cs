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
        public List<User> getListUser()
        {
            return _context.users.ToList();
        }
        public void AddUser(User model)
        {
            _context.users.Add(model);
            _context.SaveChanges();
        }
        public User GetByEmail(string email)
        {
            return _context.users.FirstOrDefault(x => x.Email == email);

        }
        public User GetUserById(int id)
        {
            return _context.users.FirstOrDefault(x => x.UserId == id);

        }
        public bool EditUser(User model)
        {
            _context.users.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public bool UpdateRefreshToken(int idUser, string refreshToken, string refreshTokenExprired)
        {
            GetUserById(idUser);
            _context.users.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
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
            var Data = _mapper.Map<List<UserDTO>>(pagedData);
            return new PagingModel<UserDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = Data,
                TotalRecords = totalRecords
            };
        }
     

    }
}
