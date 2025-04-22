using Microsoft.AspNetCore.Mvc;
using Medi.Server.Services;
using Medi.Server.Models;
using System.Collections.Generic;

[Route("api/medicalFacilities")]
[ApiController]
public class MedicalFacilityController : ControllerBase
{
    private readonly MedicalFacilityService _medicalFacilityService;

    public MedicalFacilityController(MedicalFacilityService medicalFacilityService)
    {
        _medicalFacilityService = medicalFacilityService;
    }

    [HttpGet]
    public IActionResult GetMedicalFacilities()
    {
        var users = _medicalFacilityService.GetMedicalFacilities();
        return Ok(users);
    }
}
