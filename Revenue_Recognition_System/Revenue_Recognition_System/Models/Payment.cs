namespace Revenue_Recognition_System.Models;

public class Payment
{
    public int IdPayment { get; }
    public decimal Value { get; }
    public DateOnly Date { get; }
    public int IdContract { get; }
    public Contract Contract { get; }
    public int IdClient { get; }
    public Client Client { get; }
}