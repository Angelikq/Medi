using Medi.Server.Interfaces;
using Medi.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Medi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchAppointments(AppointmentSearchCriteria criteria)
        {
            try
            {
                var slots = await _appointmentService.SearchAppointmentsAsync(criteria);
                if (slots == null || slots.Count() == 0)
                {
                    return NotFound("No available appointments found.");
                }
                return Ok(slots);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("schedule/{doctorId}")]
        public async Task<IActionResult> GetDoctorSchedule(int doctorId, [FromQuery] DateTime date)
        {
            try
            {
                var schedule = await _appointmentService.GetDoctorScheduleAsync(doctorId, date);
                if (schedule == null || schedule.Count() == 0)
                {
                    return NotFound("No schedule found for this date.");
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex.Message}");
            }
        }
      
        
    }
}
