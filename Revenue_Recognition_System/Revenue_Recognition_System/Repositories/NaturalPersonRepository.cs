using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class NaturalPersonRepository : INaturalPersonRepository
{
    public Task<NaturalPerson> GetNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}