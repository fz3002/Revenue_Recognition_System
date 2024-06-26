using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IUserRepository
{
    Task RegisterUserAsync(User newUser, CancellationToken cancellationToken);
    Task<User?> GetUserAsync(string userLogin, CancellationToken cancellationToken);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    void UpdateToken(User userToLogIn);
}