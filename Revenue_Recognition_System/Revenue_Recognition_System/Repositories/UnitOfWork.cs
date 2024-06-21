using Revenue_Recognition_System.Context;

namespace Revenue_Recognition_System.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RRSystemDbContext _context;

    public UnitOfWork(RRSystemDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public RRSystemDbContext GetDBContext()
    {
        return _context;
    }
}