using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IDiscountRepository _discountRepository;

    public ProductService(IUnitOfWork unitOfWork, ISoftwareRepository softwareRepository, IClientRepository clientRepository, IContractRepository contractRepository, IDiscountRepository discountRepository)
    {
        _unitOfWork = unitOfWork;
        _softwareRepository = softwareRepository;
        _clientRepository = clientRepository;
        _contractRepository = contractRepository;
        _discountRepository = discountRepository;
    }


    public async Task<ContractViewDTO> CreateContractAsync(ContractDTO contract, CancellationToken cancellationToken)
    {
        CheckTimespanBetweenDates(contract.EndDate);
        EnsureCorrectYearsOfSupport(contract.YearsOfSupport);
        
        var boughtSoftware = await _softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        
        EnsureSoftwareExists(boughtSoftware, contract.IdSoftware);
        
        var client = await _clientRepository.GetClientAsync(contract.IdClient, cancellationToken);
        
        EnsureClientExists(client, contract.IdClient);
        
        var clientAlreadyHasContractForThisSoftware =
            await _contractRepository.ClientHasContractForSoftwareAsync(boughtSoftware.IdSoftware, client.IdClient, cancellationToken);
        
        EnsureClientDoesntHaveContractForSoftware(clientAlreadyHasContractForThisSoftware);
        
        var discount = await _discountRepository.GetMaxDiscountAsync(cancellationToken);
        var isPrevClient = await _contractRepository.IsPrevClientAsync(contract.IdClient, cancellationToken);
        var value = Math.Round((boughtSoftware.Price + (contract.YearsOfSupport - 1) * 1000) *
                    (isPrevClient ? new decimal(0.95) : new decimal(1)) * (1 + (discount?.Value ?? 0m)),2);

        var idDiscount = discount?.IdDiscount ?? null;

        var newContract = new Contract(DateOnly.FromDateTime(contract.EndDate), idDiscount,
            contract.YearsOfSupport, boughtSoftware.IdSoftware, client.IdClient, value);

        var discountDTO = discount != null
            ? new DiscountDTO(discount.Name, discount.Offer.Name, discount.Value, discount.DateFrom, discount.DateTo)
            : null;

        await _contractRepository.CreateContractAsync(newContract, cancellationToken);
        var createdContractId = await _contractRepository.GetLastCreatedIdContractAsync(cancellationToken);
        var result = new ContractViewDTO(
            createdContractId,
            newContract.StartDate,
            newContract.EndDate,
            discountDTO,
            newContract.YearsOfSupport,
            newContract.StartDate.AddYears(newContract.YearsOfSupport),
            new SoftwareDTO(boughtSoftware.Name, boughtSoftware.Description, boughtSoftware.Version,
                boughtSoftware.Price, boughtSoftware.Category.Name),
            client.IdClient,
            new ClientDTO(client.Address, client.Email, client.PhoneNumber, null, null),
            value,
            newContract.Paid
        );
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return result;
    }

    public async Task<ContractViewDTO> GetContractAsync(int id, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetContractAsync(id, cancellationToken);
        
        EnsureContractExists(contract, id);

        var discountReference = await _discountRepository.GetDiscountAsync(contract.IdDiscount, cancellationToken);
        var softwareReference = await _softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        var clientReference = await _clientRepository.GetClientAsync(contract.IdClient, cancellationToken);
        var discountDTO = discountReference != null
            ? new DiscountDTO(discountReference.Name,
                discountReference.Offer.Name,
                discountReference.Value, discountReference.DateFrom, discountReference.DateTo)
            : null;
        
        return new ContractViewDTO
        (
            contract.IdContract,
            contract.StartDate,
            contract.EndDate,
            discountDTO,
            contract.YearsOfSupport,
            contract.StartDate.AddYears(contract.YearsOfSupport),
            new SoftwareDTO(softwareReference.Name, softwareReference.Description, softwareReference.Version,
                softwareReference.Price, softwareReference.Category.Name),
            clientReference.IdClient,
            new ClientDTO(clientReference.Address, clientReference.Email, clientReference.PhoneNumber, null, null),
            contract.Value,
            contract.Paid
            );
    }

    public async Task<PaymentViewDTO> PayForContractAsync(PaymentDTO payment, CancellationToken cancellationToken)
    {
        EnsurePaymentMoreThen0(payment.Value);
        
        var newPayment = new Payment(payment.Value, payment.IdContract, payment.IdClient);
        var contract = await _contractRepository.GetContractAsync(payment.IdContract, cancellationToken);
        
        EnsureContractExists(contract, payment.IdContract);
        EnsureContractBelongsToClient(contract, payment.IdClient);
        EnsurePaymentLessThenValue(contract, payment.Value, payment.IdContract);

        var softwareReference = await  _softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        var clientReference = await _clientRepository.GetClientAsync(contract.IdClient, cancellationToken);

        if (contract.EndDate < DateOnly.FromDateTime(DateTime.Now))
        {
            var idDiscount = contract.Discount?.IdDiscount ?? null;
            var newEndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(Math.Abs(contract.EndDate.Day - contract.StartDate.Day));
            contract = new Contract(newEndDate, idDiscount,
                contract.YearsOfSupport, softwareReference.IdSoftware, clientReference.IdClient, contract.Value);
            await _contractRepository.CreateContractAsync(contract, cancellationToken);
            var createdContractId = await _contractRepository.GetLastCreatedIdContractAsync(cancellationToken);
            newPayment = new Payment(payment.Value, createdContractId, payment.IdClient);

        }
        
        await _contractRepository.PayForContractAsync(newPayment, cancellationToken);
        _contractRepository.UpdateContractPaid(contract, payment.Value);
        await _unitOfWork.CommitAsync(cancellationToken);
        var paymentID = await _contractRepository.GetLastPaymentIdAsync(cancellationToken);
        var leftToPay = contract.Value - contract.Paid - newPayment.Value;
        
        return new PaymentViewDTO(paymentID, newPayment.Value, newPayment.IdContract, newPayment.IdClient, leftToPay);
    }

    public async Task<PaymentViewDTO?> GetPaymentAsync(int id, CancellationToken cancellationToken)
    {
        var payment = await _contractRepository.GetPaymentAsync(id, cancellationToken);
        
        EnsurePaymentExists(payment, id);
        
        var contract = await _contractRepository.GetContractAsync(payment.IdContract, cancellationToken);
        var leftToPay = contract.Value - contract.Paid - payment.Value;
        
        return new PaymentViewDTO(payment.IdPayment, payment.Value, payment.IdContract, payment.IdClient, leftToPay);
    }



    private static void CheckTimespanBetweenDates(DateTime dateTo)
    {
        var dateDifference = Math.Abs(dateTo.Day - DateTime.Now.Day);
        
        if (dateDifference is < 3 or > 30)
        {
            throw new DomainException("Wrong Timespan between start and end dates of the contract");
        }
    }
    private static void EnsureSoftwareExists(Software? boughtSoftware, int contractIdSoftware)
    {
        if (boughtSoftware == null)
        {
            throw new DomainException($"Software with id {contractIdSoftware} doesn't exist");
        }
    }
    
    private static void EnsureCorrectYearsOfSupport(int contractYearsOfSupport)
    {
        if (contractYearsOfSupport is < 1 or > 4)
        {
            throw new DomainException("Wrong Years Of Support given");
        }
    }

    private static void EnsureClientExists(Client? client, int id)
    {
        if (client == null)
        {
            throw new DomainException($"Client with id {id} doesn't exist");
        }
    }

    private static void EnsureClientDoesntHaveContractForSoftware(bool clientAlreadyHasContractForThisSoftware)
    {
        if (clientAlreadyHasContractForThisSoftware)
        {
            throw new DomainException("Client Already has active Contract for this software");
        }
    }

    private static void EnsureContractExists(Contract? contract, int id)
    {
        if (contract == null)
        {
            throw new DomainException($"Contract with id {id} doesn't exist");
        }
    }

    private static void EnsurePaymentLessThenValue(Contract contract, decimal paymentValue, int id)
    {
        if (contract.Value < (contract.Paid + paymentValue))
        {
            throw new DomainException($"Payment exceeds contract {id} value");
        }
    }

    private static void EnsurePaymentMoreThen0(decimal paymentValue)
    {
        if (paymentValue <= 0)
        {
            throw new DomainException($"Payment value needs to be more then 0");
        }
    }

    private static void EnsurePaymentExists(Payment? payment, int id)
    {
        if (payment == null)
        {
            throw new DomainException($"Payment {id} doesn't exist");
        }
    }

    private void EnsureContractBelongsToClient(Contract? contract, int paymentIdClient)
    {
        if (contract.IdClient != paymentIdClient)
        {
            throw new DomainException("Client doesn't own this contract");
        }
    }

}