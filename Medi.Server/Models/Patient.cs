using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
