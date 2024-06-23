using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IClientRepository
{
    Task<NaturalPerson?> GetNaturalPersonAsync(int id, CancellationToken cancellationToken);
    Task<Company?> GetCompanyAsync(int id, CancellationToken cancellationToken);
    Task<NaturalPerson?> GetNaturalPersonAsync(string pesel, CancellationToken cancellationToken);
    Task<Company?> GetCompanyAsync(string krs, CancellationToken cancellationToken);
    Task AddNaturalPersonAsync(NaturalPerson client, CancellationToken cancellationToken);
    Task AddCompanyAsync(Company client, CancellationToken cancellationToken);
    Task DeleteClient(NaturalPerson naturalPerson, CancellationToken cancellationToken);
    Task UpdateNaturalPersonAsync(NaturalPerson existingClient, NaturalPerson client, CancellationToken cancellationToken);
    Task UpdateCompanyAsync(Company existingClient, Company client, CancellationToken cancellationToken);
}