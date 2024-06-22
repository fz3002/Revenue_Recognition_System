using Revenue_Recognition_System.Controllers;

namespace Revenue_Recognition_System.Models;

public class Company : Client
{
    public string CompanyName { get; set; }
    public string KRS { get;}

    private Company()
    {

    }
    public Company(string krs)
    {
        KRS = krs;
    }
}