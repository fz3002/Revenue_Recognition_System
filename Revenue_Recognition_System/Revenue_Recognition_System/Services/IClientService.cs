using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface IClientService
{
    Task<NaturalPersonDisplayDTO> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken);
    Task<CompanyDisplayDTO> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken);
    Task DeleteClientAsync(int id, CancellationToken cancellationToken);
    Task UpdateClientNaturalPersonAsync(int id, NaturalPersonDTO naturalPersonDto, CancellationToken cancellationToken);
    Task UpdateClientCompanyAsync(int id, CompanyDTO companyDto, CancellationToken cancellationToken);
    Task<NaturalPersonDisplayDTO> GetClientNaturalPersonAsync(int id, CancellationToken cancellationToken);
    Task<CompanyDisplayDTO> GetClientCompanyAsync(int id, CancellationToken cancellationToken);
}