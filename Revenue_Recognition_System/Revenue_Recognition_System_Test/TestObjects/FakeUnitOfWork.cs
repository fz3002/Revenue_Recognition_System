using Revenue_Recognition_System.Context;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeUnitOfWork : IUnitOfWork
{
    public void Dispose()
    {

    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public RRSystemDbContext GetDBContext()
    {
        return null;
    }
}