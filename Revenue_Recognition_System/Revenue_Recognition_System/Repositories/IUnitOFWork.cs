using Revenue_Recognition_System.Context;

namespace Revenue_Recognition_System.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    RRSystemDbContext GetDBContext();
}