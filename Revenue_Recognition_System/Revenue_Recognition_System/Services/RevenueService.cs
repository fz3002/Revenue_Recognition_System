using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class RevenueService : IRevenueService
{
    private readonly IContractRepository _contractRepository;

    public RevenueService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }
    public async Task<decimal> GetRevenueAsync(CancellationToken cancellationToken)
    {
        return await _contractRepository.GetRevenueAsync(cancellationToken);
    }
}