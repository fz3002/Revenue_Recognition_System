namespace Revenue_Recognition_System.DTO;

public record SoftwareDTO(
    string Name,
    string Description,
    string Version,
    decimal Price,
    string CategoryName);