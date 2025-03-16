using Medi.Server.Models;
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
        public DbSet<MedicalFacility> medicalFacilities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}