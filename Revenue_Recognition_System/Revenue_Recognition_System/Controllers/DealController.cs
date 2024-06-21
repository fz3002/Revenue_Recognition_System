using Microsoft.AspNetCore.Mvc;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/Deal")]
public class DealController : ControllerBase
{
    private readonly ServiceInterfaceName _service;

    public DealController(ServiceInterfaceName service)
    {
        _service = service;
    }
}