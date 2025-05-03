using System.Globalization;
using System.Text;
using Bogus;
using Bogus.DataSets;
using CsvHelper;
using Medi.Server.Models;
using Medi.Server.Models.Enities;
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
                        Email = "jan.kowalski@example.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = "Admin"
                    },
                    new User {
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
                var users = context.Users.ToList();

                var patients = new Faker<Patient>("pl")
                    .RuleFor(p => p.User, f => f.PickRandom(users))
                    .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                    .RuleFor(p => p.LastName, f => f.Name.LastName())
                    .RuleFor(p => p.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
                    .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName))
                    .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber())
                    .Generate(10);

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }

            if (!context.Address.Any())
            {
                var cities = new[]
                {
                    new City { Name = "Legnica" }
                };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            
                var streetPrefixes = new[]
                {
                    new StreetPrefix{ Name = "ul."},
                    new StreetPrefix{ Name = "al."},
                    new StreetPrefix{ Name = "os."},
                    new StreetPrefix{ Name = "plac"}
                };

                context.StreetPrefixes.AddRange(streetPrefixes);
                context.SaveChanges();

                var postalCodes = new Faker<PostalCode>("pl")
                    .RuleFor(a => a.Name, f => f.Address.ZipCode())
                    .Generate(3);

                var streets = new Faker<Street>("pl")
                    .RuleFor(p => p.Name, f => f.Address.StreetName())
                    .Generate(20);


                context.Streets.AddRange(streets);
                context.SaveChanges();


                var addresses = new Faker<Models.Enities.Address>("pl")
                    .RuleFor(a => a.City, f => f.PickRandom(cities))
                    .RuleFor(a => a.StreetPrefix, f => f.PickRandom(streetPrefixes))
                    .RuleFor(a => a.Street, f => f.PickRandom(streets))
                    .RuleFor(a => a.BuildingNumber, f => f.Address.BuildingNumber())
                    .RuleFor(a => a.ApartamentNumber, f => f.Address.BuildingNumber())
                    .RuleFor(a => a.PostalCode, f => f.PickRandom(postalCodes))
                    .Generate(50);

                context.Address.AddRange(addresses);
                context.SaveChanges();
            }

            if (!context.MedicalFacilities.Any())
            {
                using var reader = new StreamReader("Data/clinics-data.csv", Encoding.UTF8);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Context.RegisterClassMap<MedicalFacilityMap>();

                var medicalFacilities = csv.GetRecords<MedicalFacility>().ToList();

                context.MedicalFacilities.AddRange(medicalFacilities);
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

                var medicalFacilities = context.MedicalFacilities.ToList();

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
                    .RuleFor(a => a.Patient, f => f.PickRandom(patients))
                    .RuleFor(a => a.Doctor, f => f.PickRandom(doctors))
                    .RuleFor(a => a.AppointmentDate, f => f.Date.Future(1))
                    .RuleFor(a => a.Notes, f => f.Lorem.Sentence())
                    .Generate(50);

                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }

        }
    }
}
