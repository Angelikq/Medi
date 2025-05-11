using Medi.Server.Data;
using Medi.Server.Interfaces;
using Medi.Server.Models.DTOs;
using Medi.Server.Models.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace Medi.Server.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentSlotDTO>> SearchAppointmentsAsync(AppointmentSearchCriteria criteria)
        {
            var query = _context.AppointmentSlots
                .Include(s => s.Doctor)
                .ThenInclude(d => d.Specialization)
                .Include(s => s.Doctor)
                .ThenInclude(d => d.MedicalFacility)
                .Where(slot => slot.Appointment == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.FacilityNameOrSpecialization))
            {
                query = query.Where(slot =>
                    slot.Doctor.Specialization.Name.Contains(criteria.FacilityNameOrSpecialization) ||
                    slot.Doctor.MedicalFacility.Name.Contains(criteria.FacilityNameOrSpecialization));
            }
            if (criteria.SpecializationsClicked != null && criteria.SpecializationsClicked.Any())
            {
                query = query.Where(slot => criteria.SpecializationsClicked.Contains(slot.Doctor.Specialization.Name));
            }

            if (criteria.Date.HasValue)
            {
                var dateOnly = criteria.Date.Value.Date;
                query = query.Where(slot => slot.StartTime.Date == dateOnly);
            }

            var availableSlots = await query
                .GroupBy(slot => slot.Doctor.Id)
                .Select(group => group.OrderBy(slot => slot.StartTime)
                .FirstOrDefault())
                .ToListAsync();

            if (!availableSlots.Any())
            {
                return new List<AppointmentSlotDTO>();
            }

            var result = availableSlots.Select(slot => new AppointmentSlotDTO
            {
                Id = slot.Id,
                StartTime = slot.StartTime,
                DoctorFullName = $"{slot.Doctor.FirstName} {slot.Doctor.LastName}",
                Specialization = slot.Doctor.Specialization.Name,
                DoctorId = slot.Doctor.Id,
                MedicalFacilityId = slot.Doctor.MedicalFacility.Id,
                MedicalFacilityName = slot.Doctor.MedicalFacility.Name
            }).ToList();

            return result;
        }
        public Task<IEnumerable<AppointmentSlot>> GetDoctorScheduleAsync(int doctorId, DateTime date)
        {
            throw new NotImplementedException();

        }
    }
}
