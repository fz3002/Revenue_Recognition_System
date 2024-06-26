using Microsoft.AspNetCore.Mvc;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Services;

namespace Revenue_Recognition_System.Controllers;

[ApiController]
[Route("api/logins")]
public class AuthorizationController : ControllerBase
{
    private readonly ISecurityService _service;

    public AuthorizationController(ISecurityService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(UserDto user, CancellationToken cancellationToken)
    {
        await _service.RegisterUser(user, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> LogInAsync(UserDto user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}