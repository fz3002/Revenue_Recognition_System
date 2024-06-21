using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.Services;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/Deal")]
public class DealController : ControllerBase
{
    private readonly IDealService _service;

    public DealController(IDealService service)
    {
        _service = service;
    }
}