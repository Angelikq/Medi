namespace Medi.Server.Models.DTOs
{
    public class AppointmentSearchCriteria
    {
        public string? FacilityNameOrSpecialization { get; set; }
        public DateTime? Date { get; set; }
        public List<string>? SpecializationsClicked { get; set; }
    }

}
