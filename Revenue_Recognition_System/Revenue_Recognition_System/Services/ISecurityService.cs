using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface ISecurityService
{
    Task RegisterUser(UserDto user, CancellationToken cancellationToken);
    Task<TokenDTO> LogInAsync(UserDto user, CancellationToken cancellationToken);
    Task<TokenDTO> RefreshToken(RefreshTokenDTO refreshTokenDto, CancellationToken cancellationToken);
}