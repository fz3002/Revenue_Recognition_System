using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Enums;
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
        throw new NotImplementedException();
    }
}