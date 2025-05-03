using Medi.Server.Models.Enities;
using Microsoft.EntityFrameworkCore;

namespace Medi.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalFacility> MedicalFacilities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<StreetPrefix> StreetPrefixes { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}