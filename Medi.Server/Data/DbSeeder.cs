using System.Globalization;
using System.Text;
using Bogus;
using Bogus.DataSets;
using CsvHelper;
using Medi.Server.Models;
using Medi.Server.Models.DTOs;
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

            if (!context.MedicalFacilities.Any())
            {
                var cityCache = new Dictionary<string, City>();
                var streetCache = new Dictionary<string, Street>();
                var prefixCache = new Dictionary<string, StreetPrefix>();
                var postalCache = new Dictionary<string, PostalCode>();

                using var reader = new StreamReader("Data/clinics-data.csv", Encoding.UTF8);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<MedicalFacilityMap>();

                var records = csv.GetRecords<MedicalFacilityDTO>().ToList();
                var facilities = new List<MedicalFacility>();

                foreach (var r in records)
                {
                    var city = cityCache.TryGetValue(r.City, out var c) ? c :
                        context.Cities.FirstOrDefault(x => x.Name == r.City) ?? new City { Name = r.City };

                    cityCache[r.City] = city;

                    var street = streetCache.TryGetValue(r.Street, out var s) ? s :
                        context.Streets.FirstOrDefault(x => x.Name == r.Street) ?? new Street { Name = r.Street };

                    streetCache[r.Street] = street;

                    var prefix = prefixCache.TryGetValue(r.StreetPrefix, out var p) ? p :
                        context.StreetPrefixes.FirstOrDefault(x => x.Name == r.StreetPrefix) ?? new StreetPrefix { Name = r.StreetPrefix };

                    prefixCache[r.StreetPrefix] = prefix;

                    var postal = postalCache.TryGetValue(r.PostalCode, out var pc) ? pc :
                        context.PostalCode.FirstOrDefault(x => x.Name == r.PostalCode) ?? new PostalCode { Name = r.PostalCode };

                    postalCache[r.PostalCode] = postal;

                    facilities.Add(new MedicalFacility
                    {
                        Name = r.Name,
                        Phone = r.Phone,
                        Address = new Models.Enities.Address
                        {
                            City = city,
                            Street = street,
                            StreetPrefix = prefix,
                            PostalCode = postal,
                            BuildingNumber = r.BuildingNumber,
                            ApartmentNumber = r.ApartmentNumber,
                        }
                    });
                }

                context.MedicalFacilities.AddRange(facilities);
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
