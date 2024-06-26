using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(UserDto user, CancellationToken cancellationToken)
    {
        await _service.RegisterUser(user, cancellationToken);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> LogInAsync(UserDto user, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.LogInAsync(user, cancellationToken);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized();
        }
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(RefreshTokenDTO refreshTokenDto, CancellationToken cancellationToken)
    {
        var result = await _service.RefreshToken(refreshTokenDto, cancellationToken);
        return Ok(result);
    }
}