using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface IProductService
{
    Task<ContractViewDTO> CreateContractAsync(ContractDTO contract, CancellationToken cancellationToken);
    Task<ContractViewDTO> GetContractAsync(int id, CancellationToken cancellationToken);
    Task<PaymentViewDTO> PayForContractAsync(PaymentDTO payment, CancellationToken cancellationToken);
    Task<PaymentViewDTO?> GetPaymentAsync(int id, CancellationToken cancellationToken);
}