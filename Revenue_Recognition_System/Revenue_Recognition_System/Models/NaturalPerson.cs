using Revenue_Recognition_System.Controllers;

namespace Revenue_Recognition_System.Models;

public class NaturalPerson : Client, ISoftDelete
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

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}