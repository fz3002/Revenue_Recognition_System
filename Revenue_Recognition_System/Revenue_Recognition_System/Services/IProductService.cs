using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface IProductService
{
    Task<ContractViewDTO> CreateContractAsync(ContractDTO contract, CancellationToken cancellationToken);
    Task<ContractViewDTO> GetContractAsync(int id, CancellationToken cancellationToken);
    Task<PaymentViewDTO> PayForContract(PaymentDTO payment, CancellationToken cancellationToken);
    Task<PaymentViewDTO?> GetPayment(int id, CancellationToken cancellationToken);
}