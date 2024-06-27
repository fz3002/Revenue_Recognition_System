using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface ISecurityService
{
    Task RegisterUserAsync(UserDto user, CancellationToken cancellationToken);
    Task<TokenDTO> LogInAsync(UserDto user, CancellationToken cancellationToken);
    Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO refreshTokenDto, CancellationToken cancellationToken);
}