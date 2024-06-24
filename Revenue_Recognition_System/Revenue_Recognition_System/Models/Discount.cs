namespace Revenue_Recognition_System.Models;

public class Discount
{
    public int IdDiscount { get; set; }
    public string Name { get; set; }
    public int IdDiscountType { get; set; }
    public DiscountType Offer { get; set; }
    public Decimal Value { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}