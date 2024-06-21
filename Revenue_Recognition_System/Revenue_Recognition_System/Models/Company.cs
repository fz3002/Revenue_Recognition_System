namespace Revenue_Recognition_System.Models;

public class Company
{
    public int IdCompany { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string KRS { get; private set; }

    public Company(string krs)
    {
        KRS = krs;
    }
}