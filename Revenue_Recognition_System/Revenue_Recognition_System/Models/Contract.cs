namespace Revenue_Recognition_System.Models;

public class Contract
{
    public int IdContract { get; }
    public DateOnly StartDate { get;  }
    public DateOnly EndDate { get;  }
    public decimal Paid { get; set; }
    public Discount? Discount { get;  }
    public int? IdDiscount { get;  }
    public int YearsOfSupport { get; } = 1;
    public int IdSoftware { get;  }
    public Software Software { get;  }
    public int IdClient { get;  }
    public Client Client { get;  }
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