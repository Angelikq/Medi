using Medi.Server.Models;
using Medi.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Medi.Server.Services
{
    public class MedicalFacilityService
    {
        private readonly ApplicationDbContext _context; 

        public MedicalFacilityService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
