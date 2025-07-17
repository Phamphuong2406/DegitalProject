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
        public void AddUser(User model)
        {
            _context.users.Add(model);
            _context.SaveChanges();
        }
        public bool GetByEmail(string email)
        {
            var user = _context.users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public User GetUserById(int id)
        {
            return _context.users.FirstOrDefault(x => x.UserId == id);

        }
        public bool EditUser(User model)
        {

            _context.users.Update(model);
           var result= _context.SaveChanges();
            return result > 0;
        }
        public void DeleteUser(int id)
        {

        }

    }
}
