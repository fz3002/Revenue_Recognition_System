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
    public async Task<IActionResult> GetRevenueAsync(CancellationToken cancellationToken, [FromQuery] int idSoftware = -1, [FromQuery] string? currency = null)
    {
        var result = await _service.GetRevenueAsync(idSoftware, currency, cancellationToken);
        return Ok(result);
    }

    [HttpGet("expected")]
    public async Task<IActionResult> GetExpectedRevenue(CancellationToken cancellationToken)
    {
        var result = await _service.GetExpectedRevenueAsync(cancellationToken);
        return Ok(result);
    }
}