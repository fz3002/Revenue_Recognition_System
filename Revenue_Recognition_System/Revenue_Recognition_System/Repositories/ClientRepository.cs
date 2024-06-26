using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<NaturalPerson?> GetNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().People
            .Where(p => p.IdClient == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Company?> GetCompanyAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Companies
            .Where(p => p.IdClient == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<NaturalPerson?> GetNaturalPersonAsync(string pesel, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().People
            .Where(p => p.Pesel == pesel)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Company?> GetCompanyAsync(string krs, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Companies
            .Where(p => p.KRS == krs)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddNaturalPersonAsync(NaturalPerson client, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().People.AddAsync(client, cancellationToken);
    }

    public async Task AddCompanyAsync(Company client, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().Companies.AddAsync(client, cancellationToken);
    }

    public async Task DeleteClient(NaturalPerson naturalPerson, CancellationToken cancellationToken)
    {
        _unitOfWork.GetDBContext().People.Remove(naturalPerson);
    }

    public async Task UpdateNaturalPersonAsync(NaturalPerson existingClient, NaturalPerson client, CancellationToken cancellationToken)
    {
        existingClient.Name = client.Name;
        existingClient.Surname = client.Surname;
        existingClient.PhoneNumber = client.PhoneNumber;
        existingClient.Address = client.Address;
        existingClient.Email = client.Email;
    }

    public async Task UpdateCompanyAsync(Company existingClient, Company client, CancellationToken cancellationToken)
    {
        existingClient.CompanyName = client.CompanyName;
        existingClient.PhoneNumber = client.PhoneNumber;
        existingClient.Address = client.Address;
        existingClient.Email = client.Email;
    }

    public async Task<Client?> GetClientAsync(int contractIdClient, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Clients
            .Include(c => c.Contracts)
            .Include(c => c.Payments)
            .Where(c => c.IdClient == contractIdClient)
            .FirstOrDefaultAsync(cancellationToken);
    }
}