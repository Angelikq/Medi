using CsvHelper.Configuration;
using Medi.Server.Models;
using Medi.Server.Models.Enities;

public class MedicalFacilityMap : ClassMap<MedicalFacility>
{
    public MedicalFacilityMap()
    {
        Map(m => m.Name).Index(0);
        Map(m => m.Address).Convert(row => new Address
        {
            StreetPrefix = new StreetPrefix { Name = row.Row.GetField<string>(2) },
            Street = new Street { Name = row.Row.GetField<string>(3) },
            BuildingNumber = row.Row.GetField<string>(4),
            PostalCode = new PostalCode { Name = row.Row.GetField<string>(5) },
            City = new City { Name = row.Row.GetField<string>(1) }
        });
    }
}
