namespace Revenue_Recognition_System.DTO;

public record ContractDTO(
    DateTime EndDate,
    int YearsOfSupport,
    int IdSoftware,
    int IdClient);