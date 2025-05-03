using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models.DTOs
{
    public class MedicalFacilityDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetPrefix { get; set; }
        public string BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string PostalCode { get; set; }

    }
}
