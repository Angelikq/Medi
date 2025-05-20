namespace Medi.Server.Models.DTOs
{
    public class AppointmentSearchCriteria
    {
        public string? DoctorNameOrSpecialization { get; set; }
        public DateTime? Date { get; set; }
        public string? Specialization { get; set; }
    }

}
