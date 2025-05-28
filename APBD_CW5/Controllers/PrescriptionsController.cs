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
    
    [HttpPost]
    public async Task<IActionResult> AddPrescriptionAsync([FromBody] PrescriptionPostGto prescriptionPostGto)
    {
        try
        {
            return Ok(await service.AddPrescriptionAsync(prescriptionPostGto));
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
}