using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Helpers;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class UserRepository : IUserRepository
{

    private IUnitOfWork _unitOfWork;

    public UserRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterUserAsync(User newUser, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().Users.AddAsync(newUser, cancellationToken);
    }

    public async Task<User?> GetUserAsync(string userLogin, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Users.Where(u => u.Login == userLogin)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Users.Where(u => u.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void UpdateToken(User userToLogIn)
    {
        userToLogIn.RefreshToken = AuthorizationHelpers.GenerateRefreshToken();
        userToLogIn.RefreshTokenExp = DateTime.Now.AddDays(1);
    }
}