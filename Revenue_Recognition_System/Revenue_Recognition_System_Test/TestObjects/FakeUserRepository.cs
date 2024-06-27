using Revenue_Recognition_System.Enums;
using Revenue_Recognition_System.Helpers;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeUserRepository : IUserRepository
{
    private ICollection<User> _users;

    public FakeUserRepository()
    {

        var hashedPasswordAndSalt = AuthorizationHelpers.GetHashedPasswordAndSalt("Password");
        var hashedPasswordAndSalt1 = AuthorizationHelpers.GetHashedPasswordAndSalt("Password1");
        var hashedPasswordAndSalt2 = AuthorizationHelpers.GetHashedPasswordAndSalt("Password2");

        _users = new List<User>()
        {
            new User
            {
                IdUser = 1,
                Login = "Login 1",
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                Role = Role.User,
                RefreshToken = AuthorizationHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(2)
            },
            new User
            {
                IdUser = 2,
                Login = "Login 2",
                Password = hashedPasswordAndSalt1.Item1,
                Salt = hashedPasswordAndSalt1.Item2,
                Role = Role.User,
                RefreshToken = AuthorizationHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(2)
            },
            new User
            {
                IdUser = 3,
                Login = "Login 3",
                Password = hashedPasswordAndSalt2.Item1,
                Salt = hashedPasswordAndSalt2.Item2,
                Role = Role.User,
                RefreshToken = AuthorizationHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.MinValue

            }
        };
    }

    public async Task RegisterUserAsync(User newUser, CancellationToken cancellationToken)
    {
        _users.Add(newUser);
    }

    public async Task<User?> GetUserAsync(string userLogin, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_users
            .FirstOrDefault(u => u.Login == userLogin));
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_users
            .FirstOrDefault(u => u.RefreshToken == refreshToken));
    }

    public void UpdateToken(User userToLogIn)
    {
        userToLogIn.RefreshToken = AuthorizationHelpers.GenerateRefreshToken();
        userToLogIn.RefreshTokenExp = DateTime.Now.AddDays(1);
    }
}