using Revenue_Recognition_System.Enums;

namespace Revenue_Recognition_System.DTO;

public record ClientDTO(string Address, string Email, string PhoneNumber, ClientType Type, TypeSpecificPropertiesDTO Properties);