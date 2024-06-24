using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface ISoftwareRepository
{
    Task<Software?> GetSoftwareAsync(int id, CancellationToken cancellationToken);
}