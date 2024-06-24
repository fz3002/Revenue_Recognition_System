using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IContractRepository
{
    Task<bool> IsPrevClientAsync(int contractIdClient, CancellationToken cancellationToken);
    Task<bool> ClientHasContractForSoftwareAsync(Software? boughtSoftware, Client? client, CancellationToken cancellationToken);
    Task CreateContractAsync(Contract newContract, CancellationToken cancellationToken);
    Task<int> GetLastCreatedIdContractAsync(CancellationToken cancellationToken);
}