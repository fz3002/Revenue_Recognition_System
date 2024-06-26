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

    public async Task<bool> ClientHasContractForSoftwareAsync(int? boughtSoftware, int? client, CancellationToken cancellationToken)
    {
        //Assuming offer has 4 states (active, inactive, paid, finished) and is active only for time it can be paid for,
        //becoming paid if whole sum was paid in time and inactive is not
        return await _unitOfWork.GetDBContext().Contracts
            .AnyAsync(c =>
                c.IdClient == client
                && c.IdSoftware == boughtSoftware
                && c.StartDate <= DateOnly.FromDateTime(DateTime.Now)
                && DateOnly.FromDateTime(DateTime.Now) <= c.EndDate
                && c.Paid < c.Value,
                cancellationToken);
    }

    public async Task CreateContractAsync(Contract newContract, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().Contracts.AddAsync(newContract, cancellationToken);
    }

    public async Task<int> GetLastCreatedIdContractAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts
            .OrderByDescending(c => c.IdContract)
            .Select(c => c.IdContract)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Contract?> GetContractAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts
            .Include(c => c.Discount)
            .Include(c => c.Software)
            .Include(c => c.Client)
            .Where(c => c.IdContract == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task PayForContractAsync(Payment newPayment, CancellationToken cancellationToken)
    {
        await _unitOfWork.GetDBContext().Payments.AddAsync(newPayment, cancellationToken);
    }

    public void UpdateContractPaid(Contract contract, decimal paymentValue)
    {
        contract.Paid += paymentValue;
    }

    public async Task<int> GetLastPaymentIdAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Payments
            .OrderByDescending(c => c.IdPayment)
            .Select(c => c.IdPayment)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Payments
            .Where(p => p.IdPayment == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<decimal> GetRevenueAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts
            .Where(c => c.Paid == c.Value)
            .SumAsync(c => c.Value, cancellationToken);
    }

    public async Task<decimal> GetExpectedRevenue(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Contracts
            .SumAsync(c => c.Value, cancellationToken);
    }
}