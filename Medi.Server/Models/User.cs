using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Rola jest wymagana.")]
        public string Role { get; set; }
    }
}
