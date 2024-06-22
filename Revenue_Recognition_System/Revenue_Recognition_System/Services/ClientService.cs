using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class ClientService : IClientService
{
    private ICompanyRepository _companyRepository;
    private INaturalPersonRepository _naturalPersonRepository;
    private IUnitOfWork _unitOfWork;

    public ClientService(ICompanyRepository companyRepository, INaturalPersonRepository naturalPersonRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _naturalPersonRepository = naturalPersonRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<NaturalPersonDTO> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<NaturalPersonDTO> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClientNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClientCompanyAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClientNaturalPersonAsync(int id, NaturalPersonDTO naturalPersonDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClientCompanyAsync(int id, CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<NaturalPersonDTO> GetClientNaturalPerson(int id, CancellationToken cancellationToken)
    {
        var result = await _naturalPersonRepository.GetNaturalPersonAsync(id, cancellationToken);
        return null;
    }

    public Task<NaturalPersonDTO> GetClientCompany(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}