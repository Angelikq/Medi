using Bogus;
using Medi.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Medi.Server.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User {
                        Name = "Jan Kowalski",
                        Email = "jan.kowalski@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = "Admin"
                    },
                    new User {
                        Name = "Anna Nowak",
                        Email = "anna.nowak@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = "Admin"
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Patients.Any())
            {

                var patients = new Faker<Patient>("pl")
                    .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                    .RuleFor(p => p.LastName, f => f.Name.LastName())
                    .RuleFor(p => p.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
                    .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName))
                    .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber())
                    .Generate(10);

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                var cities = new[]
                {
                    new City { Name = "Warsaw" },
                    new City { Name = "Krakow" },
                    new City { Name = "Wroclaw" },
                    new City { Name = "Gdansk" },
                    new City { Name = "Poznan" }
                };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }

            if (!context.medicalFacilities.Any())
            {
                var cities = context.Cities.ToList();

                var medicalFacilityFaker = new Faker<MedicalFacility>()
                    .RuleFor(m => m.Name, f => f.Company.CompanyName())
                    .RuleFor(m => m.City.Id, f => f.PickRandom(cities).Id)
                    .RuleFor(m => m.Address, f => f.Address.StreetAddress())
                    .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber())
                    .Generate(5);

                context.medicalFacilities.AddRange(medicalFacilityFaker);
                context.SaveChanges();
            }

            if (!context.Doctors.Any())
            {
                var specializations = new[]
                {
                    new Specialization { Name = "Kardiolog" },
                    new Specialization { Name = "Neurolog" },
                    new Specialization { Name = "Ortopeda" }
                };

                context.Specializations.AddRange(specializations);
                context.SaveChanges();

                var medicalFacilities = context.medicalFacilities.ToList();

                var doctorFaker = new Faker<Doctor>("pl")
                    .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                    .RuleFor(d => d.LastName, f => f.Name.LastName())
                    .RuleFor(d => d.Email, f => f.Internet.Email())
                    .RuleFor(d => d.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(d => d.SpecializationId, f => f.PickRandom(specializations).Id)
                    .RuleFor(d => d.MedicalFacilityId, f => f.PickRandom(medicalFacilities).Id)
                    .Generate(5);

                context.Doctors.AddRange(doctorFaker);
                context.SaveChanges();
            }

            if (!context.Appointments.Any())
            {
                var patients = context.Patients.ToList();
                var doctors = context.Doctors.ToList();

                var appointments = new Faker<Appointment>("pl")
                    .RuleFor(a => a.PatientId, f => f.PickRandom(patients).Id)
                    .RuleFor(a => a.DoctorId, f => f.PickRandom(doctors).Id)
                    .RuleFor(a => a.AppointmentDate, f => f.Date.Future(1))
                    .RuleFor(a => a.Notes, f => f.Lorem.Sentence())
                    .Generate(50);

                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }

            if (!context.Prescriptions.Any())
            {
                var appointments = context.Appointments.ToList();

                var prescriptions = new Faker<Prescription>("pl")
                    .RuleFor(pr => pr.AppointmentId, f => f.PickRandom(appointments).Id)
                    .RuleFor(pr => pr.Medications, f => f.Lorem.Words(3).Aggregate((a, b) => a + ", " + b))
                    .RuleFor(pr => pr.Dosage, f => f.Random.Number(1, 3) + " razy dziennie")
                    .Generate(30);

                context.Prescriptions.AddRange(prescriptions);
                context.SaveChanges();
            }
        }
    }
}
