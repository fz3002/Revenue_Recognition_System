using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface INaturalPersonRepository
{
    Task<NaturalPerson> GetNaturalPersonAsync(int id, CancellationToken cancellationToken);
}