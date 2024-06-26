using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.DTO;

public record ContractViewDTO(
    int IdContract,
    DateOnly StartDate,
    DateOnly EndDate,
    DiscountDTO? Discount,
    int YearsOfSupport,
    DateOnly EndOfSupport,
    SoftwareDTO Software,
    int IdClient,
    ClientDTO Client,
    decimal Cost,
    decimal Paid
    );