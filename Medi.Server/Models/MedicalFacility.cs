using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models
{
    public class MedicalFacility
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
