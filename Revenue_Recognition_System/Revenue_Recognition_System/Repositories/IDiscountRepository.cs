using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetMaxDiscountAsync(CancellationToken cancellationToken);
    Task<DiscountType?> GetDiscountTypeAsync(Discount? discount, CancellationToken cancellationToken);
}