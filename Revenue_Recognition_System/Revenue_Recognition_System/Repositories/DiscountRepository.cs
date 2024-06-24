using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private IUnitOfWork _unitOfWork;
    private const string DiscountTypeOneTypePurchase = "Discount for one time purchase";

    public DiscountRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Discount?> GetMaxDiscountAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Discounts
            .Include(d => d.Offer)
            .Where(d =>
            d.Offer.Name.Equals(DiscountTypeOneTypePurchase) &&
            d.DateFrom < DateOnly.FromDateTime(DateTime.Now) &&
            DateOnly.FromDateTime(DateTime.Now) < d.DateTo)
            .OrderByDescending(d => d.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DiscountType?> GetDiscountTypeAsync(Discount? discount, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetDBContext().Discounts
            .Include(d => d.Offer).Select(d => d.Offer).FirstOrDefaultAsync(cancellationToken);
    }
}