namespace Revenue_Recognition_System.Services;

public interface IRevenueService
{
    Task<decimal> GetRevenueAsync(CancellationToken cancellationToken);
    Task<decimal> GetExpectedRevenueAsync(CancellationToken cancellationToken);
}