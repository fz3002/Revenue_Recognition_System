using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.Services;

namespace Revenue_Recognition_System.Controllers;


[ApiController]
[Route("api/revenue")]
public class RevenueController : ControllerBase
{
    private IRevenueService _service;

    public RevenueController(IRevenueService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetRevenueAsync(CancellationToken cancellationToken)
    {
        var result = await _service.GetRevenueAsync(cancellationToken);
        return Ok(result);
    }
}