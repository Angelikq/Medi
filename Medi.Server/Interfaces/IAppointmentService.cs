using Medi.Server.Models.DTOs;
using Medi.Server.Models.Enities;

namespace Medi.Server.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentSlotDTO>> SearchAppointmentsAsync(AppointmentSearchCriteria criteria);
        Task<IEnumerable<AppointmentSlot>> GetDoctorScheduleAsync(int doctorId, DateTime date);

    }
}
