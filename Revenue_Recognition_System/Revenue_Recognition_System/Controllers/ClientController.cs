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
    
    [HttpPost()]
    public async Task<IActionResult> AddClientAsync(ClientDTO clientDto, CancellationToken cancellationToken)
    {
        var result = await _service.AddClientAsync(clientDto, cancellationToken);
         return CreatedAtRoute("GetClientNaturalPerson", new { id = result.IdClient }, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteClientAsync(id, cancellationToken);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateClientAsync(int id, ClientDTO clientDTO, CancellationToken cancellationToken)
    {
        await _service.UpdateClientAsync(id, clientDTO, cancellationToken);
        return Ok();
    }

    [HttpGet("{id:int}", Name = "GetClient")]
    public async Task<IActionResult> GetClientAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientAsync(id, cancellationToken);
        return Ok(result);
    }
}
