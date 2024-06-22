using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface IClientService
{
    Task<NaturalPersonDTO> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken);
    Task<NaturalPersonDTO> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken);
    Task DeleteClientNaturalPersonAsync(int id, CancellationToken cancellationToken);
    Task DeleteClientCompanyAsync(int id, CancellationToken cancellationToken);
    Task UpdateClientNaturalPersonAsync(int id, NaturalPersonDTO naturalPersonDto, CancellationToken cancellationToken);
    Task UpdateClientCompanyAsync(int id, CompanyDTO companyDto, CancellationToken cancellationToken);
    Task<NaturalPersonDTO> GetClientNaturalPerson(int id, CancellationToken cancellationToken);
    Task<NaturalPersonDTO> GetClientCompany(int id, CancellationToken cancellationToken);
}