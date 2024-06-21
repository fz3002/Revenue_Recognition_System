namespace Revenue_Recognition_System.Models;

public class NaturalPerson
{
    public int IdPerson { get; private set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Pesel { get; }

    private NaturalPerson()
    {

    }

    public NaturalPerson(string pesel)
    {
        Pesel = pesel;
    }
}