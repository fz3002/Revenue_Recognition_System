using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Services;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/clients")]

public class ClientController : ControllerBase
{
    private readonly IClientService _service;

    public ClientController(IClientService service)
    {
        _service = service;
    }
    //TODO: Fix CreatedAtAction
    [HttpPost("/naturalPeople")]
    public async Task<IActionResult> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken)
    {
        var result = await _service.AddClientNaturalPersonAsync(personDto, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("/companies")]
    public async Task<IActionResult> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        var result = await _service.AddClientCompanyAsync(companyDto, cancellationToken);
        return CreatedAtAction(result);
    }

    [HttpDelete("/naturalPeople/{id:int}")]
    public async Task<IActionResult> DeleteClientNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteClientNaturalPersonAsync(id, cancellationToken);
        return Ok();
    }

    [HttpDelete("/companies/{id:int}")]
    public async Task<IActionResult> DeleteClientCompanyAsync(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteClientCompanyAsync(id, cancellationToken);
        return Ok();
    }

    [HttpPut("/naturalPeople/{id:int}")]
    public async Task<IActionResult> UpdateClientNaturalPersonAsync(int id, NaturalPersonDTO naturalPersonDTO, CancellationToken cancellationToken)
    {
        await _service.UpdateClientNaturalPersonAsync(id, naturalPersonDTO, cancellationToken);
        return Ok();
    }

    [HttpPut("/companies/{id:int}")]
    public async Task<IActionResult> UpdateClientCompany(int id, CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        await _service.UpdateClientCompanyAsync(id, companyDto, cancellationToken);
        return Ok();
    }

    [HttpGet]
}