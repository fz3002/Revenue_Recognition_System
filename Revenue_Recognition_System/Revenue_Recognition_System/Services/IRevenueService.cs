namespace Revenue_Recognition_System.Services;

public interface IRevenueService
{
    Task<decimal> GetRevenueAsync(int idSoftware, string? currency, CancellationToken cancellationToken);
    Task<decimal> GetExpectedRevenueAsync(CancellationToken cancellationToken);
}