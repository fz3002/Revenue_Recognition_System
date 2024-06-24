using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Services;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/contracts")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContractAsync([FromBody] ContractDTO contract,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateContractAsync(contract, cancellationToken);
        return CreatedAtRoute("GetContract", new { id = result.IdContract }, result);
    }

    [HttpGet("{id:int}", Name = "GetContract")]
    public async Task<IActionResult> GetContractAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _service.GetContractAsync(id, cancellationToken);
        return Ok(result);
    }
}