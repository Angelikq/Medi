using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models.Enities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public AppointmentSlot AppointmentSlot { get; set; }
        public string Notes { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
