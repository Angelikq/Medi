using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public Prescription Prescription { get; set; }
    }
}
