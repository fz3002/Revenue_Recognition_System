using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Models;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class ProductService : IProductService
{
    private IUnitOfWork _unitOfWork;
    private ISoftwareRepository _softwareRepository;
    private IClientRepository _clientRepository;
    private IContractRepository _contractRepository;
    private IDiscountRepository _discountRepository;

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
        CheckTimespanBetweenDates(contract.StartDate, contract.EndDate);
        EnsureCorrectYearsOfSupport(contract.YearsOfSupport);
        var boughtSoftware = await _softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        EnsureSoftwareExists(boughtSoftware, contract.IdSoftware);
        var client = await _clientRepository.GetClientAsync(contract.IdClient, cancellationToken);
        EnsureClientExists(client, contract.IdClient);
        var clientAlreadyHasContractForThisSoftware =
            await _contractRepository.ClientHasContractForSoftwareAsync(boughtSoftware, client, cancellationToken);
        EnsureClientDoesntHaveContractForSoftware(clientAlreadyHasContractForThisSoftware);
        var discount = await _discountRepository.GetMaxDiscountAsync(cancellationToken);
        var discountType = await _discountRepository.GetDiscountTypeAsync(discount, cancellationToken);
        var isPrevClient = await _contractRepository.IsPrevClientAsync(contract.IdClient, cancellationToken);
        var value = (boughtSoftware.Price + (contract.YearsOfSupport - 1) * 1000) *
                    (isPrevClient ? new decimal(1.05) : new decimal(1)) * (1 + discount.Value);

        var newContract = new Contract(contract.StartDate, contract.EndDate, discount.IdDiscount,
            contract.YearsOfSupport, boughtSoftware.IdSoftware, client.IdClient, value);

        await _contractRepository.CreateContractAsync(newContract, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        var createdContractId = await _contractRepository.GetLastCreatedIdContractAsync(cancellationToken);

        return new ContractViewDTO(
            createdContractId,
            newContract.StartDate,
            newContract.EndDate,
            new DiscountDTO(discount.Name, discountType.Name, discount.Value, discount.DateFrom, discount.DateTo),
            newContract.YearsOfSupport,
            newContract.StartDate.AddYears(newContract.YearsOfSupport),
            new SoftwareDTO(boughtSoftware.Name, boughtSoftware.Description, boughtSoftware.Version,
                boughtSoftware.Price, boughtSoftware.Category.Name),
            client.IdClient,
            new ClientDTO(client.Address, client.Email, client.PhoneNumber, null, null),
            value
        );
    }

    public Task<ContractViewDTO> GetContractAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static void CheckTimespanBetweenDates(DateOnly dateFrom, DateOnly dateTo)
    {
        var dateDifference = Math.Abs(dateTo.DayNumber - dateFrom.DayNumber);
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
}