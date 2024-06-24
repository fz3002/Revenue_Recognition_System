using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class ContractRepository : IContractRepository
{
    private IUnitOfWork _unitOfWork;

    public ContractRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> IsPrevClientAsync(int contractIdClient, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts.AnyAsync(contract => contract.IdClient == contractIdClient, cancellationToken);
    }

    public async Task<bool> ClientHasContractForSoftwareAsync(Software? boughtSoftware, Client? client, CancellationToken cancellationToken)
    {
        //Assuming offer has 4 states (active, inactive, paid, finished) and is active only for time it can be paid for,
        //becoming paid if whole sum was paid in time and inactive is not
        return await _unitOfWork.GetDBContext().Contracts
            .AnyAsync(c =>
                c.Client == client
                && c.Software == boughtSoftware
                && c.StartDate < DateOnly.FromDateTime(DateTime.Now)
                && DateOnly.FromDateTime(DateTime.Now) < c.StartDate
                && c.Paid < c.Value,
                cancellationToken);
    }

    public async Task CreateContractAsync(Contract newContract, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().Contracts.AddAsync(newContract, cancellationToken);
    }

    public async Task<int> GetLastCreatedIdContractAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts.OrderByDescending(c => c.IdContract).Select(c => c.IdContract)
            .FirstOrDefaultAsync(cancellationToken);
    }
}