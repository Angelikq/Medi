using System.Globalization;
using System.Text;
using Bogus;
using CsvHelper;
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
                var specializations = new[] { "Internista", "Pediatra", "Lekarz rodzinny", "Kardiolog", "Endokrynolog", "Neurolog", "Ortopeda", "Reumatolog", "Dermatolog", "Chirurg", "Ginekolog", "Urolog", "Laryngolog", "Okulista", "Psychiatra", "Stomatolog", "Onkolog", "Pulmonolog", "Alergolog", "Anestezjolog", "Hematolog", "Geriatra", "Medycyna pracy", "Chirurg plastyczny", "Chirurg naczyniowy" };
                var specializationsList = specializations.Select(name => new Specialization { Name = name }).ToList();
                context.Specializations.AddRange(specializationsList);
                context.SaveChanges();

                var medicalFacilities = context.MedicalFacilities.ToList();

                var doctorFaker = new Faker<Doctor>("pl")
                    .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                    .RuleFor(d => d.LastName, f => f.Name.LastName())
                    .RuleFor(d => d.Email, f => f.Internet.Email())
                    .RuleFor(d => d.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(d => d.Specialization, f => f.PickRandom(specializationsList))
                    .RuleFor(d => d.MedicalFacility, f => f.PickRandom(medicalFacilities))
                    .RuleFor(d => d.WorkingHours, f =>
                    {
                        var workingDaysCount = f.Random.Int(2, 6);

                        var workingDays = f.PickRandom(Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList(), workingDaysCount);

                        return workingDays.Select(day => new WorkingHours
                        {
                            DayOfWeek = day,
                            StartTime = new TimeSpan(f.Random.Int(6, 9), f.PickRandom(0, 30), 0),
                            EndTime = new TimeSpan(f.Random.Int(12, 18), f.PickRandom(0, 30), 0)
                        }).ToList();
                    })
                    .Generate(120);

                context.Doctors.AddRange(doctorFaker);
                context.SaveChanges();
            }

            if (!context.AppointmentSlots.Any())
            {
                var doctors = context.Doctors.Include(d => d.WorkingHours).ToList();
                var slots = new List<AppointmentSlot>();
                var startDate = DateTime.Today;
                var endDate = startDate.AddDays(28);

                foreach (var doctor in doctors)
                {
                    foreach (var workingHour in doctor.WorkingHours)
                    {
                        var currentDate = startDate;
                        while (currentDate <= endDate)
                        {
                            if (currentDate.DayOfWeek == workingHour.DayOfWeek)
                            {
                                var currentTime = currentDate.Date + workingHour.StartTime;
                                var endTime = currentDate.Date + workingHour.EndTime;

                                while (currentTime + TimeSpan.FromMinutes(30) <= endTime)
                                {
                                    slots.Add(new AppointmentSlot
                                    {
                                        Doctor = doctor,
                                        StartTime = currentTime,
                                        DurationInMinutes = 30,
                                    });
                                    currentTime = currentTime.AddMinutes(30);
                                }
                            }
                            currentDate = currentDate.AddDays(1);
                        }
                    }
                }

                context.AppointmentSlots.AddRange(slots);
                context.SaveChanges();
            }

            if (!context.Appointments.Any())
            {
                var patients = context.Patients.ToList();
                var freeSlots = context.AppointmentSlots.Include(s => s.Doctor).ToList();

                var appointments = new List<Appointment>();

                foreach (var slot in freeSlots.Take(150))
                {
                    var patient = new Faker().PickRandom(patients);

                    var appointment = new Appointment
                    {
                        Patient = patient,
                        Doctor = slot.Doctor,
                        AppointmentSlot = slot,
                        Notes = "Wizyta kontrolna"
                    };
                    slot.Appointment = appointment;
                    appointments.Add(appointment);
                }

                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}
