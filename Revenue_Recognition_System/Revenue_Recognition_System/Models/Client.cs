namespace Revenue_Recognition_System.Models;

public abstract class Client : ISoftDelete
{
    public int IdClient { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Payment> Payments { get; set; }
}