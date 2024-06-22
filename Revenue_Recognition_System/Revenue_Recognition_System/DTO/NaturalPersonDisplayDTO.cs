namespace Revenue_Recognition_System.DTO;

public record NaturalPersonDisplayDTO(int IdClient, string Name, string Surname, string Address, string Email, string PhoneNumber, string Pesel)
    : ClientDTO(IdClient, Address, Email, PhoneNumber);