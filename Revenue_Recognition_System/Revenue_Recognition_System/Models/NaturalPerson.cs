using Revenue_Recognition_System.Controllers;

namespace Revenue_Recognition_System.Models;

public class NaturalPerson : Client
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Pesel { get; }

    private NaturalPerson()
    {

    }

    public NaturalPerson(string pesel)
    {
        Pesel = pesel;
    }
}