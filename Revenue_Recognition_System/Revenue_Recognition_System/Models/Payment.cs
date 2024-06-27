namespace Revenue_Recognition_System.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public decimal Value { get; }
    public DateOnly Date { get; }
    public int IdContract { get; }
    public Contract Contract { get; }
    public int IdClient { get; }
    public Client Client { get; }

    public Payment(decimal value, int idContract, int idClient)
    {
        Value = value;
        Date = DateOnly.FromDateTime(DateTime.Now);
        IdContract = idContract;
        IdClient = idClient;
    }
}