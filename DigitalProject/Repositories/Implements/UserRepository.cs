using DigitalProject.Entitys;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Interface;

namespace DigitalProject.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
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
            var user = _context.users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public User GetUserById(int id)
        {
            var user = _context.users.FirstOrDefault(x => x.UserId == id);
            if (user == null) {
                return null;
            }
            return user;
        }
        public bool EditUser(User model)
        {

            _context.users.Update(model);
           var result= _context.SaveChanges();
            return result > 0;
        }
        public void DeleteUser(User model)
        {
            _context.users.Remove(model);
            _context.SaveChanges();
        }
        public List<User> GetUserByKey(string? key, bool IsActive)
        {
            var query = _context.users.Where(x => x.IsActive == IsActive);

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.UserName.Contains(key) || x.Email.Contains(key));
            }

            return query.Select( x=> new User
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Email = x.Email,
                IsActive = x.IsActive,
                FullName = x.FullName,
                note = x.note,
                PhoneNumber = x.PhoneNumber,
            }
            
            ).ToList();
        }
        public bool UpdateRefreshToken(User model)
        {
            _context.users.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
        }

    }
}
