using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Medications { get; set; }
        public string Dosage { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}
