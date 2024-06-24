namespace Revenue_Recognition_System.DTO;

public record ContractDTO(
    DateOnly StartDate,
    DateOnly EndDate,
    int YearsOfSupport,
    int IdSoftware,
    int IdClient);