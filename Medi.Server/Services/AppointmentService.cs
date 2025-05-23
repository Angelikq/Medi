using Medi.Server.Data;
using Medi.Server.Interfaces;
using Medi.Server.Models.DTOs;
using Medi.Server.Models.Enities;
using Microsoft.EntityFrameworkCore;
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
                .Where(slot => slot.Appointment == null && slot.StartTime > DateTime.Now)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.DoctorNameOrSpecialization))
            {
                var search = criteria.DoctorNameOrSpecialization.ToLower();

                query = query.Where(slot =>
                    (slot.Doctor.FirstName + " " + slot.Doctor.LastName).ToLower().Contains(search) ||
                    slot.Doctor.LastName.ToLower().Contains(search) ||
                    slot.Doctor.MedicalFacility.Name.ToLower().Contains(search));
            }
            if (!string.IsNullOrEmpty(criteria.Specialization))
            {
                query = query.Where(slot => slot.Doctor.Specialization.Name.Contains(criteria.Specialization));
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
        public async Task<List<string>> GetDoctorAndSpecializationSuggestionsAsync(string query)
        {
            query = query.ToLower();
            var suggestionsDoctors = await _context.Doctors
                .Where(d => d.FirstName.Contains(query) || d.LastName.Contains(query))
                .Select(d => $"{d.FirstName} {d.LastName}")
                .ToListAsync();
            var specializationSuggestions = await _context.Specializations
                .Where(s => s.Name.Contains(query))
                .Select(s => s.Name)
                .ToListAsync();

            var result = suggestionsDoctors.Union(specializationSuggestions);
            if (result.Any())
            {

                return result.Distinct().OrderBy(name => name).ToList();
            }
            return new List<string>();
        }
    }
}
