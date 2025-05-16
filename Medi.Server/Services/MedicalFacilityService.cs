using Medi.Server.Data;
using Microsoft.EntityFrameworkCore;
using Medi.Server.Models.Enities;

namespace Medi.Server.Services
{
    public class MedicalFacilityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MedicalFacilityService> _logger;

        public MedicalFacilityService(ApplicationDbContext context, ILogger<MedicalFacilityService> logger  )
        {
            _context = context;
            _logger = logger;
        }

        public List<MedicalFacility> GetMedicalFacilities()
        {
            return _context.MedicalFacilities
           .Include(mf => mf.Address)
           .Include(a => a.Address.StreetPrefix)
           .Include(a => a.Address.Street)
           .Include(a => a.Address.City)
           .Include(a => a.Address.PostalCode)
           .Include(mf => mf.Doctors)
           .Include(mf => mf.User)
           .ToList();
        }
        public async Task<string?> GetSimpleAddressMedFac(int facilityId)
        {
            _logger.LogInformation("Pobieranie uproszczonego adresu dla placówki ID: {Id}", facilityId);

            var facility = await _context.MedicalFacilities
                .Where(f => f.Id == facilityId)
                .Select(f => new
                {
                    StreetName = f.Address.Street.Name,
                    f.Address.BuildingNumber,
                    PostalCode = f.Address.PostalCode.Name,
                    CityName = f.Address.City.Name
                })
                .FirstOrDefaultAsync();

            if (facility == null)
            {
                _logger.LogWarning("Nie znaleziono placówki o ID: {Id}", facilityId);
                return null;
            }

            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(facility.StreetName)) parts.Add(facility.StreetName);
            if (!string.IsNullOrWhiteSpace(facility.BuildingNumber)) parts.Add(facility.BuildingNumber);
            if (!string.IsNullOrWhiteSpace(facility.PostalCode)) parts.Add(facility.PostalCode);
            if (!string.IsNullOrWhiteSpace(facility.CityName)) parts.Add(facility.CityName);

            var result = string.Join(" ", parts);

            _logger.LogInformation("Zbudowany adres: {Address}", result);
            return result;
        }    
    }
}
