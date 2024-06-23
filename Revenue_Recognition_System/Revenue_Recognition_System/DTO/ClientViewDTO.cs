using Revenue_Recognition_System.Enums;

namespace Revenue_Recognition_System.DTO;

public record ClientViewDTO(int IdClient, string Address, string Email, string PhoneNumber, ClientType Type, Dictionary<string, string> TypeSpecificProperties);