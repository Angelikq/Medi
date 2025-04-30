using Medi.Server.Models;
using Medi.Server.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medi.Server.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ServiceResult<object> Register(AuthRequest model)
        {
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                return new ServiceResult<object>
                {
                    Success = false,
                    Message = "Użytkownik z podanym adresem e-mail już istnieje.",
                    ErrorType = ErrorType.Duplicate
                };
            }

            var user = new User
            {
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Pacjent"
            };

            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorMessages = validationResults.Select(vr => vr.ErrorMessage).ToList();
                return new ServiceResult<object>
                {
                    Success = false,
                    Message = string.Join(", ", errorMessages),
                    ErrorType = ErrorType.Validation
                };
            }

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return new ServiceResult<object>
                {
                    Success = false,
                    Message = "Wystąpił błąd serwera podczas zapisu do bazy danych.",
                    ErrorType = ErrorType.Database
                };
            }

            return new ServiceResult<object>
            {
                Success = true,
                Message = "Rejestracja zakończona sukcesem."
            };
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList(); 
        }

        public ServiceResult<LoginResponse> Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new ServiceResult<LoginResponse>
                {
                    Success = false,
                    Message = "Błędne dane logowania",
                    ErrorType = ErrorType.Authentication
                };
            }

            var token = GenerateJwtToken(user);

            return new ServiceResult<LoginResponse>
            {
                Success = true,
                Message = "Zalogowano!",
                Data = new LoginResponse { Token = token }
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
