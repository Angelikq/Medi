using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace Medi.Server.Models.Enities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public StreetPrefix StreetPrefix { get; set; }
        [Required]
        public Street Street { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public PostalCode PostalCode { get; set; }
    }
}
