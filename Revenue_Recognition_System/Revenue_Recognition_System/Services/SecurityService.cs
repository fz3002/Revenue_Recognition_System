using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Enums;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Helpers;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class SecurityService : ISecurityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public SecurityService(IUnitOfWork unitOfWork, IConfiguration configuration, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task RegisterUser(UserDto user, CancellationToken cancellationToken)
    {
        var controlUser = await _userRepository.GetUserAsync(user.Login, cancellationToken);
        EnsureUserDoesntExist(controlUser);
        var hashedPasswordAndSalt = AuthorizationHelpers.GetHashedPasswordAndSalt(user.Password);

        var newUser = new User
        {
            Login = user.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            Role = Role.User,
            RefreshToken = AuthorizationHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(2)
        };

        await _userRepository.RegisterUserAsync(newUser, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<TokenDTO> LogInAsync(UserDto user, CancellationToken cancellationToken)
    {
        var userToLogIn = await _userRepository.GetUserAsync(user.Login, cancellationToken);
        EnsureUserExists(userToLogIn);
        var passwordHash = user.Password;
        var givenHashPass = AuthorizationHelpers.GetHashedPasswordWithSalt(user.Password, userToLogIn.Salt);

        if (passwordHash != givenHashPass)
        {
            throw new UnauthorizedAccessException();
        }

        var token = CreateToken();

        _userRepository.UpdateToken(userToLogIn);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new TokenDTO(new JwtSecurityTokenHandler().WriteToken(token), userToLogIn.RefreshToken);
    }


    public async Task<TokenDTO> RefreshToken(RefreshTokenDTO refreshTokenDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshTokenDto.RefreshToken, cancellationToken);
        EnsureRefreshTokenIntegrity(user);
        EnsureRefreshTokenUpToDate(user);
        var token = CreateToken();

        _userRepository.UpdateToken(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new TokenDTO(new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
    }

    private JwtSecurityToken CreateToken()
    {
        var userClaim = new[]
        {
            new Claim(ClaimTypes.Role, Role.User.ToString())
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenIssuer"],
            audience: _configuration["TokenAudience"],
            claims: userClaim,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: credentials
        );
        return token;
    }

    private static void EnsureUserExists(User? user)
    {
        if (user == null)
        {
            throw new DomainException("User with this login doesn't exist");
        }
    }

    private static void EnsureUserDoesntExist(User? user)
    {
        if (user != null)
        {
            throw new DomainException("User with this login already exists");
        }
    }

    private static void EnsureRefreshTokenIntegrity(User? user)
    {
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    }

    private static void EnsureRefreshTokenUpToDate(User? user)
    {
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
    }
}