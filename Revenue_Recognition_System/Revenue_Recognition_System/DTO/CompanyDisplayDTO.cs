namespace Revenue_Recognition_System.DTO;

public record CompanyDisplayDTO(int IdClient, string Address, string Email, string PhoneNumber,string CompanyName, string KRS)
    : ClientDTO(IdClient, Address, Email, PhoneNumber);