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
}