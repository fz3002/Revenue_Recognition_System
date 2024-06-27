using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeClientRepository : IClientRepository
{
    private ICollection<NaturalPerson> _naturalPersons;
    private ICollection<Company> _companies;
    private ICollection<Client> _clients;

    public FakeClientRepository()
    {
        _naturalPersons = new List<NaturalPerson>()
        {
            new NaturalPerson("12345678901")
            {
                IdClient = 3,
                Name = "John",
                Surname = "Doe",
                Address = "123 Main St",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            },
            new NaturalPerson("09876543210")
            {
                IdClient = 4,
                Name = "Jane",
                Surname = "Smith",
                Address = "456 Elm St",
                Email = "jane.smith@example.com",
                PhoneNumber = "098-765-4321"
            }
        };
        _companies = new List<Company>()
        {
            new Company("1234567890")
            {
                IdClient = 1,
                CompanyName = "Company One",
                Address = "123 Main St",
                Email = "info@companyone.com",
                PhoneNumber = "123-456-7890"
            },
            new Company("0987654321")
            {
                IdClient = 2,
                CompanyName = "Company Two",
                Address = "456 Elm St",
                Email = "info@companytwo.com",
                PhoneNumber = "098-765-4321"
            }
        };
        _clients = new List<Client>();
        _clients.Add(_companies.ElementAt(0));
        _clients.Add(_companies.ElementAt(1));
        _clients.Add(_naturalPersons.ElementAt(0));
        _clients.Add(_naturalPersons.ElementAt(1));
    }

    public async Task<NaturalPerson?> GetNaturalPersonAsync(int id, CancellationToken cancellationToken)
    {
        var naturalPerson = _naturalPersons
            .FirstOrDefault(p => p.IdClient == id);

        return await Task.FromResult(naturalPerson);
    }

    public async Task<Company?> GetCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var company = _companies
            .FirstOrDefault(p => p.IdClient == id);

        return await Task.FromResult(company);
    }

    public async Task<NaturalPerson?> GetNaturalPersonAsync(string pesel, CancellationToken cancellationToken)
    {
        var naturalPerson = _naturalPersons
            .FirstOrDefault(p => p.Pesel == pesel);

        return await Task.FromResult(naturalPerson);
    }

    public async Task<Company?> GetCompanyAsync(string krs, CancellationToken cancellationToken)
    {
        var company = _companies
            .FirstOrDefault(p => p.KRS == krs);

        return await Task.FromResult(company);
    }

    public async Task AddNaturalPersonAsync(NaturalPerson client, CancellationToken cancellationToken)
    {
        _naturalPersons.Add(client);
    }

    public async Task AddCompanyAsync(Company client, CancellationToken cancellationToken)
    {
        _companies.Add(client);
    }

    public async Task DeleteClient(NaturalPerson naturalPerson, CancellationToken cancellationToken)
    {
        _naturalPersons.Remove(naturalPerson);
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
        var client = _clients
            .FirstOrDefault(c => c.IdClient == contractIdClient);

        return await Task.FromResult(client);
    }
}