using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IUserRepository
{
    Task RegisterUserAsync(User newUser, CancellationToken cancellationToken);
}