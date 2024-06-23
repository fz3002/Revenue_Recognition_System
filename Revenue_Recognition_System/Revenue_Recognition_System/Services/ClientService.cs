using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Enums;
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

    public async Task<ClientViewDTO> AddClientAsync(ClientDTO clientDto, CancellationToken cancellationToken)
    {
        CheckClientDtoFormating(clientDto);
        switch (clientDto.Type)
        {
            case ClientType.NaturalPerson:
            {
                var controlClient = await _clientRepository.GetNaturalPersonAsync(clientDto.Properties.Pesel, cancellationToken);
                EnsureClientNotInDatabase(controlClient);
                var client = new NaturalPerson(clientDto.Properties.Pesel)
                {
                    Address = clientDto.Address,
                    Email = clientDto.Email,
                    Name = clientDto.Properties.Name,
                    PhoneNumber = clientDto.PhoneNumber,
                    Surname = clientDto.Properties.Surname
                };
                await _clientRepository.AddNaturalPersonAsync(client, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                var createdClient = await _clientRepository.GetNaturalPersonAsync(clientDto.Properties.Pesel, cancellationToken);
                return new ClientViewDTO
                (
                    createdClient.IdClient,
                    createdClient.Address,
                    createdClient.Email,
                    createdClient.PhoneNumber,
                    ClientType.NaturalPerson,
                    new Dictionary<string, string>()
                    {
                        {"Name", createdClient.Name},
                        {"Surname", createdClient.Surname},
                        {"PESEL", createdClient.Pesel}
                    }
                );
            }
            case ClientType.Company:
            {
                var controlClient = await _clientRepository.GetCompanyAsync(clientDto.Properties.Krs, cancellationToken);
                EnsureClientNotInDatabase(controlClient);
                var client = new Company(clientDto.Properties.Krs)
                {
                    Address = clientDto.Address,
                    Email = clientDto.Email,
                    CompanyName = clientDto.Properties.Name,
                    PhoneNumber = clientDto.PhoneNumber
                };
                await _clientRepository.AddCompanyAsync(client, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                var createdClient = await _clientRepository.GetCompanyAsync(clientDto.Properties.Krs, cancellationToken);
                return new ClientViewDTO
                (
                    createdClient.IdClient,
                    createdClient.Address,
                    createdClient.Email,
                    createdClient.PhoneNumber,
                    ClientType.NaturalPerson,
                    new Dictionary<string, string>()
                    {
                        {"CompanyName", createdClient.CompanyName},
                        {"KRS", createdClient.KRS}
                    }
                );
            }
            default:
                return null;
        }
    }

    public async Task DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        var clientCompany = await _clientRepository.GetCompanyAsync(id, cancellationToken);
        EnsureNotACompany(clientCompany);
        var clientToDelete = await _clientRepository.GetNaturalPersonAsync(id, cancellationToken);
        EnsureClientExists(clientToDelete, id);
        await _clientRepository.DeleteClient(clientToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task UpdateClientAsync(int id, ClientDTO clientDto, CancellationToken cancellationToken)
    {
        CheckClientDtoFormating(clientDto);
        switch (clientDto.Type)
        {
            case ClientType.NaturalPerson:
            {
                var existingClient = await _clientRepository.GetNaturalPersonAsync(id, cancellationToken);
                EnsureClientExists(existingClient, id);
                var client = new NaturalPerson(clientDto.Properties.Pesel)
                {
                    Address = clientDto.Address,
                    Email = clientDto.Email,
                    Name = clientDto.Properties.Name,
                    PhoneNumber = clientDto.PhoneNumber,
                    Surname = clientDto.Properties.Surname
                };
                await _clientRepository.UpdateNaturalPersonAsync(existingClient, client, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                break;
            }
            case ClientType.Company:
            {
                var existingClient = await _clientRepository.GetCompanyAsync(id, cancellationToken);
                EnsureClientExists(existingClient, id);
                var client = new Company(clientDto.Properties.Krs)
                {
                    Address = clientDto.Address,
                    Email = clientDto.Email,
                    CompanyName = clientDto.Properties.Name,
                    PhoneNumber = clientDto.PhoneNumber
                };
                await _clientRepository.UpdateCompanyAsync(existingClient, client, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task<ClientViewDTO> GetClientAsync(int id, CancellationToken cancellationToken)
    {
        var resultNaturalPerson = await _clientRepository.GetNaturalPersonAsync(id, cancellationToken);
        var resultCompany = await _clientRepository.GetCompanyAsync(id, cancellationToken);
        EnsureClientExists(resultNaturalPerson, resultCompany, id);
        if (resultNaturalPerson != null)
            return new ClientViewDTO
            (
                resultNaturalPerson.IdClient,
                resultNaturalPerson.Address,
                resultNaturalPerson.Email,
                resultNaturalPerson.PhoneNumber,
                ClientType.NaturalPerson,
                new Dictionary<string, string>()
                {
                    {"Name", resultNaturalPerson.Name},
                    {"Surname", resultNaturalPerson.Surname},
                    {"PESEL", resultNaturalPerson.Pesel}
                }
            );
        if (resultCompany != null)
            return new ClientViewDTO
            (
                resultCompany.IdClient,
                resultCompany.Address,
                resultCompany.Email,
                resultCompany.PhoneNumber,
                ClientType.NaturalPerson,
                new Dictionary<string, string>()
                {
                    {"CompanyName", resultCompany.CompanyName},
                    {"KRS", resultCompany.KRS}
                }
            );
        return null;
    }

    private void EnsureClientExists(Client? client, int id)
    {
        if (client == null)
        {
            throw new DomainException($"Client with id {id} doesn't exist");
        }
    }

    private void EnsureClientExists(Client? naturalPerson, Client? company, int id)
    {
        if (naturalPerson == null && company == null)
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

    private void EnsureNotACompany(Client? client)
    {
        if (client != null)
        {
            throw new DomainException($"Cannot delete client company");
        }
    }

    private void CheckClientDtoFormating(ClientDTO clientDto)
    {
        if (clientDto.Type == ClientType.NaturalPerson)
        {

            if (clientDto.Properties.Pesel == null || clientDto.Properties.Name == null ||
                clientDto.Properties.Surname == null)
                throw new DomainException("Empty properties values that should be given");
        }
        else if (clientDto.Type == ClientType.Company)
        {
            if (clientDto.Properties.CompanyName == null || clientDto.Properties.Krs == null)
                throw new DomainException("Empty properties values that should be given");
        }
    }
}