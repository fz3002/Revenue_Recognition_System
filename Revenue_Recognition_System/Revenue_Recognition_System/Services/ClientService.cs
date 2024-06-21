using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public class ClientService : IClientService
{
    public Task<int> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClientNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}