using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System_Test.TestObjects;

public class FakeDiscountRepository : IDiscountRepository
{
    private ICollection<Discount> _discounts;
    private ICollection<DiscountType> _discountTypes;
    private const string DiscountTypeOneTypePurchase = "Discount for one time purchase";


    public FakeDiscountRepository()
    {
        _discountTypes = new List<DiscountType>
        {
            new DiscountType
            {
                IdDiscountType = 1, Name = "Discount for one time purchase"
            },
            new DiscountType
            {
                IdDiscountType = 2, Name = "Discount for subscription"
            }
        };
        
        _discounts = new List<Discount>
        {
            new Discount
            {
                IdDiscount = 1,
                Name = "Spring Sale",
                IdDiscountType = _discountTypes.ElementAt(0).IdDiscountType,
                Offer = _discountTypes.ElementAt(0),
                Value = 0.10m, // 10% discount
                DateFrom = new DateOnly(2024, 3, 1),
                DateTo = new DateOnly(2024, 3, 31),
                Contracts = new List<Contract>()
            },
            new Discount
            {
                IdDiscount = 2,
                Name = "Black Friday",
                IdDiscountType = _discountTypes.ElementAt(1).IdDiscountType,
                Offer = _discountTypes.ElementAt(1),
                Value = 0.50m, // $50 discount
                DateFrom = new DateOnly(2024, 11, 29),
                DateTo = new DateOnly(2024, 11, 29),
                Contracts = new List<Contract>()
            },
            new Discount
            {
                IdDiscount = 3,
                Name = "Cyber Monday",
                IdDiscountType = _discountTypes.ElementAt(0).IdDiscountType,
                Offer = _discountTypes.ElementAt(0),
                Value = 0.15m, // 15% discount
                DateFrom = new DateOnly(2024, 12, 2),
                DateTo = new DateOnly(2024, 12, 2),
                Contracts = new List<Contract>()
            }
        };
    }
    
    public async Task<Discount?> GetMaxDiscountAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_discounts
            .Where(d =>
                d.Offer.Name.Equals(DiscountTypeOneTypePurchase) &&
                d.DateFrom < DateOnly.FromDateTime(DateTime.Now) &&
                DateOnly.FromDateTime(DateTime.Now) < d.DateTo).MaxBy(d => d.Value));
    }

    public async Task<Discount?> GetDiscountAsync(int? id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_discounts
            .FirstOrDefault(d => d.IdDiscount == id));
    }
}