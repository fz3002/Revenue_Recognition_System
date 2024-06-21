using Microsoft.AspNetCore.Mvc;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/Client")]

public class ClientController : ControllerBase
{
    private readonly ServiceInterfaceName _service;

    public ClientController(ServiceInterfaceName service)
    {
        _service = service;
    }
}