using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models.Enities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public Specialization Specialization { get; set; }
        public MedicalFacility MedicalFacility { get; set; }
        public ICollection<WorkingHours> WorkingHours { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
