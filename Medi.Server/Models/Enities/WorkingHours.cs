using System.ComponentModel.DataAnnotations;

namespace Medi.Server.Models.Enities
{
    public class WorkingHours
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
    }
}
