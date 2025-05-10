namespace Medi.Server.Models.Enities
{
    public class AppointmentSlot
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }

    }
}
