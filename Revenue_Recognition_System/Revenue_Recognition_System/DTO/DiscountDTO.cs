namespace Revenue_Recognition_System.DTO;

public record DiscountDTO(
    string Name,
    string OfferName,
    decimal Value,
    DateOnly DateFrom,
    DateOnly DateTo
    );