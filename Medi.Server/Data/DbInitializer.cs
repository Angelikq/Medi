using Medi.Server.Models;

namespace Medi.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User {
                    Name = "Jan Kowalski", 
                    Email = "jan.kowalski@example.com", 
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin" },
                new User { Name = "Anna Nowak", 
                    Email = "anna.nowak@example.com", 
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

        }
    }
}
