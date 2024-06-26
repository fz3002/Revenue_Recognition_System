using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetMaxDiscountAsync(CancellationToken cancellationToken);
    Task<Discount?> GetDiscountAsync(int id, CancellationToken cancellationToken);
}