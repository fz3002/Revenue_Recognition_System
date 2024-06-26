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
        var value = (boughtSoftware.Price + (contract.YearsOfSupport - 1) * 1000) *
                    (isPrevClient ? new decimal(0.95) : new decimal(1)) * (1 + discount.Value);

        var newContract = new Contract(DateOnly.FromDateTime(contract.EndDate), discount.IdDiscount,
            contract.YearsOfSupport, boughtSoftware.IdSoftware, client.IdClient, value);

        await _contractRepository.CreateContractAsync(newContract, cancellationToken);
        var createdContractId = await _contractRepository.GetLastCreatedIdContractAsync(cancellationToken);
        var result = new ContractViewDTO(
            createdContractId,
            newContract.StartDate,
            newContract.EndDate,
            new DiscountDTO(discount.Name, discount.Offer.Name, discount.Value, discount.DateFrom, discount.DateTo),
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
        var boughtSoftware = await _softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        var client = await _clientRepository.GetClientAsync(contract.IdClient, cancellationToken);
        var discount = await _discountRepository.GetDiscountAsync(id, cancellationToken);
        return new ContractViewDTO
        (
            contract.IdContract,
            contract.StartDate,
            contract.EndDate,
            new DiscountDTO(discount.Name, discount.Offer.Name, discount.Value, discount.DateFrom, discount.DateTo),
            contract.YearsOfSupport,
            contract.StartDate.AddYears(contract.YearsOfSupport),
            new SoftwareDTO(boughtSoftware.Name, boughtSoftware.Description, boughtSoftware.Version,
                boughtSoftware.Price, boughtSoftware.Category.Name),
            client.IdClient,
            new ClientDTO(client.Address, client.Email, client.PhoneNumber, null, null),
            contract.Value,
            contract.Paid
            );
    }

    public async Task<PaymentViewDTO> PayForContract(PaymentDTO payment, CancellationToken cancellationToken)
    {
        EnsurePaymentMoreThen0(payment.Value);
        var newPayment = new Payment(payment.Value, payment.IdContract, payment.IdClient);
        var contract = await _contractRepository.GetContractAsync(payment.IdContract, cancellationToken);
        EnsureContractExists(contract, payment.IdContract);
        EnsurePaymentLessThenValue(contract, payment.Value, payment.IdContract);
        if (contract.EndDate < DateOnly.FromDateTime(DateTime.Now))
        {
            var newEndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(Math.Abs(contract.EndDate.Day - contract.StartDate.Day));
            contract = new Contract(newEndDate, contract.Discount.IdDiscount,
                contract.YearsOfSupport, contract.Software.IdSoftware, contract.Client.IdClient, contract.Value);
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



    public async Task<PaymentViewDTO?> GetPayment(int id, CancellationToken cancellationToken)
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
}