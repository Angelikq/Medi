namespace Medi.Server.Models.DTOs
{
    public class AppointmentSlotDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string DoctorFullName { get; set; }
        public string Specialization { get; set; }
        public int DoctorId { get; set; }
        public string MedicalFacilityName { get; set; }
        public int MedicalFacilityId { get; set; }
    }
}
