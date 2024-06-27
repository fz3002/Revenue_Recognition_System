namespace Revenue_Recognition_System.Models;

public class Contract
{
    public int IdContract { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Paid { get; set; }
    public Discount? Discount { get; set; }
    public int? IdDiscount { get;  }
    public int YearsOfSupport { get; } = 1;
    public int IdSoftware { get;  }
    public Software Software { get; set; }
    public int IdClient { get;  }
    public Client Client { get; set; }
    public decimal Value { get;  }

    public ICollection<Payment> Payments { get; set; }

    private Contract()
    {

    }

    public Contract(DateOnly endDate, int? idDiscount, int yearsOfSupport, int idSoftware, int idClient, decimal value)
    {
        StartDate = DateOnly.FromDateTime(DateTime.Now);
        EndDate = endDate;
        IdDiscount = idDiscount;
        YearsOfSupport = yearsOfSupport;
        IdSoftware = idSoftware;
        IdClient = idClient;
        Value = value;
    }
}