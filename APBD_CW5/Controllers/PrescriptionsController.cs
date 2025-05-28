using APBD_CW5.DTOs;
using APBD_CW5.Exceptions;
using APBD_CW5.Models;
using APBD_CW5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await service.GetPrescriptionsAsync());
    }
    
    [HttpPut]
    public async Task<IActionResult> AddPrescriptionAsync([FromBody] PrescriptionPutDto prescriptionPutDto)
    {
        try
        {
            return Ok(await service.AddPrescriptionAsync(prescriptionPutDto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientInfoAsync([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}