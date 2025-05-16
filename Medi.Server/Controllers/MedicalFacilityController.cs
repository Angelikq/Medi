using Microsoft.AspNetCore.Mvc;
using Medi.Server.Services;
using Medi.Server.Models;
using System.Collections.Generic;

[Route("api/medicalFacilities")]
[ApiController]
public class MedicalFacilityController : ControllerBase
{
    private readonly MedicalFacilityService _medicalFacilityService;
    private readonly ILogger<MedicalFacilityController> _logger;

    public MedicalFacilityController(MedicalFacilityService medicalFacilityService, ILogger<MedicalFacilityController> logger)
    {
        _medicalFacilityService = medicalFacilityService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetMedicalFacilities()
    {
        var mf = _medicalFacilityService.GetMedicalFacilities();
        return Ok(mf);
    }
    [HttpGet("simple-address/{medicalFacilityId}")]
    public async Task<IActionResult> GetSimpleAddressMedFac(int medicalFacilityId)
    {
        _logger.LogInformation("Żądanie adresu dla placówki ID: {Id}", medicalFacilityId);

        if (medicalFacilityId == null || medicalFacilityId <= 0)
        {
            _logger.LogWarning("Nieprawidłowe ID placówki: {Id}", medicalFacilityId);

            return BadRequest("Brak lub nieprawidłowy identyfikator.");
        }

        var address = await _medicalFacilityService.GetSimpleAddressMedFac(medicalFacilityId);
        if (address == null)
        {
            _logger.LogWarning("Brak adresu dla placówki ID: {Id}", medicalFacilityId);
            return NotFound($"Nie znaleziono placówki o ID {medicalFacilityId}");
        }

        return Ok(address);
    }
}
