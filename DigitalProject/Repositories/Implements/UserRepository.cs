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
        public bool GetByUserId(string email)
        {
            var user = _context.users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
