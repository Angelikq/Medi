using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? SpecializationId { get; set; }
        public int? MedicalFacilityId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Specialization Specialization { get; set; }
        public MedicalFacility MedicalFacility { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
