namespace Revenue_Recognition_System.Models;

public class Contract
{
    public int IdContract { get; }
    public DateOnly StartDate { get;  }
    public DateOnly EndDate { get;  }
    public bool Paid { get; set; }
    public Discount? Discount { get;  }
    public int IdDiscount { get;  }
    public int YearsOfSupport { get; } = 1;
    public int IdSoftware { get;  }
    public Software Software { get;  }
    public int IdClient { get;  }
    public Client Client { get;  }
    public Decimal Value { get;  }

    public ICollection<Payment> Payments { get; set; }

    private Contract()
    {
        
    }

    public Contract(DateOnly startDate, DateOnly endDate, int idDiscount, int yearsOfSupport, int idSoftware, int idClient, int value)
    {
        StartDate = startDate;
        EndDate = endDate;
        IdDiscount = idDiscount;
        YearsOfSupport = yearsOfSupport;
        IdSoftware = idSoftware;
        IdClient = idClient;
        Value = value;
    }
}