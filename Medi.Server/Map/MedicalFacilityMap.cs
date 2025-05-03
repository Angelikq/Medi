using CsvHelper.Configuration;
using Medi.Server.Models;
using Medi.Server.Models.DTOs;

public class MedicalFacilityMap : ClassMap<MedicalFacilityDTO>
{
    public MedicalFacilityMap()
    {
        Map(m => m.Name).Index(0);
        Map(m => m.City).Index(1);
        Map(m => m.StreetPrefix).Index(2);
        Map(m => m.Street).Index(3);
        Map(m => m.BuildingNumber).Index(4);
        Map(m => m.ApartmentNumber).Index(5);
        Map(m => m.PostalCode).Index(6);
    }
}
