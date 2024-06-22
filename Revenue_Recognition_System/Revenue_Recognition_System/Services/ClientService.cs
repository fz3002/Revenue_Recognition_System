using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class ClientService : IClientService
{
    private IClientRepository _clientRepository;
    private IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NaturalPersonDisplayDTO> AddClientNaturalPersonAsync(NaturalPersonDTO personDto, CancellationToken cancellationToken)
    {
        var controlClient = await _clientRepository.GetNaturalPersonAsync(personDto.Pesel, cancellationToken);
        EnsureClientNotInDatabase(controlClient);
        var client = new NaturalPerson(personDto.Pesel)
        {
            Address = personDto.Address,
            Email = personDto.Email,
            Name = personDto.Name,
            PhoneNumber = personDto.PhoneNumber,
            Surname = personDto.Surname
        };
        await _clientRepository.AddNaturalPersonAsync(client, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        var createdClient = await _clientRepository.GetNaturalPersonAsync(personDto.Pesel, cancellationToken);
        return new NaturalPersonDisplayDTO
        (
            createdClient.IdClient,
            createdClient.Name,
            createdClient.Surname,
            createdClient.Address,
            createdClient.Email,
            createdClient.PhoneNumber,
            createdClient.Pesel
            );
    }

    public async Task<CompanyDisplayDTO> AddClientCompanyAsync(CompanyDTO companyDto, CancellationToken cancellationToken)
    {
        var controlClient = await _clientRepository.GetCompanyAsync(companyDto.Krs, cancellationToken);
        EnsureClientNotInDatabase(controlClient);
        var client = new Company(companyDto.Krs)
        {
            Address = companyDto.Address,
            Email = companyDto.Email,
            CompanyName = companyDto.Name,
            PhoneNumber = companyDto.PhoneNumber
        };
        await _clientRepository.AddCompanyAsync(client, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        var createdClient = await _clientRepository.GetCompanyAsync(companyDto.Krs, cancellationToken);
        return new CompanyDisplayDTO
        (
            createdClient.IdClient,
            createdClient.Address,
            createdClient.Email,
            createdClient.PhoneNumber,
            createdClient.CompanyName,
            createdClient.KRS
        );
    }

    public Task DeleteClientAsync(int id, CancellationToken cancellationToken)
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

    public async Task<NaturalPersonDisplayDTO> GetClientNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _clientRepository.GetNaturalPersonAsync(id, cancellationToken);
        EnsureClientExists(result, id);
        return new NaturalPersonDisplayDTO
        (
            result.IdClient,
            result.Name,
            result.Surname,
            result.Address,
            result.Email,
            result.PhoneNumber,
            result.Pesel
            );
    }

    public async Task<CompanyDisplayDTO> GetClientCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _clientRepository.GetCompanyAsync(id, cancellationToken);
        EnsureClientExists(result, id);
        return new CompanyDisplayDTO
        (
            result.IdClient,
            result.Address,
            result.Email,
            result.PhoneNumber,
            result.CompanyName,
            result.KRS
        );
    }

    private void EnsureClientExists(Client? client, int id)
    {
        if (client == null)
        {
            throw new DomainException($"Client with id {id} doesn't exist");
        }
    }

    private void EnsureClientNotInDatabase(Client? client)
    {
        if (client != null)
        {
            throw new DomainException($"Client already exists");
        }
    }
}