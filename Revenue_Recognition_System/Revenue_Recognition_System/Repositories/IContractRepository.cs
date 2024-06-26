using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IContractRepository
{
    Task<bool> IsPrevClientAsync(int contractIdClient, CancellationToken cancellationToken);
    Task<bool> ClientHasContractForSoftwareAsync(int? boughtSoftware, int? client, CancellationToken cancellationToken);
    Task CreateContractAsync(Contract newContract, CancellationToken cancellationToken);
    Task<int> GetLastCreatedIdContractAsync(CancellationToken cancellationToken);
    Task<Contract?> GetContractAsync(int id, CancellationToken cancellationToken);
    Task PayForContractAsync(Payment newPayment, CancellationToken cancellationToken);
    void UpdateContractPaid(Contract contract, decimal paymentValue);
    Task<int> GetLastPaymentIdAsync(CancellationToken cancellationToken);
    Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken);
    Task<decimal> GetRevenueAsync(CancellationToken cancellationToken);
    Task<decimal> GetRevenueAsync(int idSoftware, CancellationToken cancellationToken);
    Task<decimal> GetExpectedRevenue(CancellationToken cancellationToken);
}