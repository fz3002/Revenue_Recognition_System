namespace Revenue_Recognition_System.Models;

public class DiscountType
{
    public int IdDiscountType { get; set; }
    public string Name { get; set; }

    public ICollection<Discount> Discounts { get; set; }
}