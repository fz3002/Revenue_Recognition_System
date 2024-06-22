using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.DTO;
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
    //TODO: FIX ROUT NOT WORKING
    [HttpPost("naturalPeople")]
    public async Task<IActionResult> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken)
    {
        var result = await _service.AddClientNaturalPersonAsync(personDto, cancellationToken);
         return CreatedAtAction(nameof(GetClientNaturalClientAsync), new { id = result.IdClient }, result);
    }

    [HttpPost("companies")]
    public async Task<IActionResult> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        var result = await _service.AddClientCompanyAsync(companyDto, cancellationToken);
        return CreatedAtAction(nameof(AddClientCompanyAsync), new {id = result.IdClient}, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteClientAsync(id, cancellationToken);
        return Ok();
    }

    [HttpPut("naturalPeople/{id:int}")]
    public async Task<IActionResult> UpdateClientNaturalPersonAsync(int id, NaturalPersonDTO naturalPersonDTO, CancellationToken cancellationToken)
    {
        await _service.UpdateClientNaturalPersonAsync(id, naturalPersonDTO, cancellationToken);
        return Ok();
    }

    [HttpPut("companies/{id:int}")]
    public async Task<IActionResult> UpdateClientCompanyAsync(int id, CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        await _service.UpdateClientCompanyAsync(id, companyDto, cancellationToken);
        return Ok();
    }

    [HttpGet("naturalPeople/{id:int}", Name = "GetClientNaturalPerson")]
    public async Task<IActionResult> GetClientNaturalClientAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientNaturalPersonAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("companies/{id:int}")]
    public async Task<IActionResult> GetClientCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientCompanyAsync(id, cancellationToken);
        return Ok(result);
    }

}
