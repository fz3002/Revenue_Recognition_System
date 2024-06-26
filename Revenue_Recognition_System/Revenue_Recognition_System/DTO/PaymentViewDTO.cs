namespace Revenue_Recognition_System.DTO;

public record PaymentViewDTO(int IdPayment, decimal Value, int IdContract, int IdClient, decimal LeftToPay);