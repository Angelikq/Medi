using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class MedicalFacility
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
