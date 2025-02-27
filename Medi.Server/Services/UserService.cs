using Medi.Server.Models;
using Medi.Server.Data;

namespace Medi.Server.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context; 

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Register(AuthRequest model)
        {
            if (_context.Users.Any(u => u.Email == model.Email)) 
            {
                return false;
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Pacjent"
            };

            _context.Users.Add(user);
            _context.SaveChanges(); 
            return true;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList(); 
        }
    }
}
