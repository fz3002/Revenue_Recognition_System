using Revenue_Recognition_System_Test.TestObjects;
using Revenue_Recognition_System.DTO;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Repositories;
using Revenue_Recognition_System.Services;
using Shouldly;

namespace Revenue_Recognition_System_Test;

public class ProductServiceTest
{
    private IProductService _service;

    public ProductServiceTest()
    {
        _service = new ProductService(new FakeUnitOfWork(), new FakeSoftwareRepository(), new FakeClientRepository(),
            new FakeContractRepository(), new FakeDiscountRepository());
    }

    [Fact]
    public async Task CreateContractAsync_TimeSpanUnder3Days_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 6, 28), 3, 1, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_TimeSpanMoreThen30Days_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 10, 28), 3, 1, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_MoreThen4YearsOfSupport_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 7, 10), 123, 1, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_SoftwareNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 7, 2), 3, 11231, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_ClientNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 7, 2), 3, 1, 12312);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_ClientHasContractForSoftwareActive_ShouldThrowDomainException()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 7, 2), 3, 1, 3);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task CreateContractAsync_AllGivenDataPassValidation_ShouldReturnContractViewDTO()
    {
        //Arrange
        var contract = new ContractDTO(new DateTime(2024, 7, 2), 3, 2, 3);

        //Assert and Act
        await Should.NotThrowAsync(_service.CreateContractAsync(contract, CancellationToken.None));
    }

    [Fact]
    public async Task GetContractAsync_ContractNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var contractId = 123123;

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.GetContractAsync(contractId, CancellationToken.None));
    }

    [Fact]
    public async Task GetContractAsync_ContractInDatabase_ShouldReturnContractViewDTO()
    {
        //Arrange
        var contractId = 4;

        //Assert and Act
        await Should.NotThrowAsync(_service.GetContractAsync(contractId, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_PayLessThen0_ShouldThrowDomainException()
    {
        //Arrange
        var payment = new PaymentDTO(0, 1, 3);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.PayForContractAsync(payment, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_ContractNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var payment = new PaymentDTO(200, 12312, 3);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.PayForContractAsync(payment, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_ContractDoesntBelongToGivenClient_ShouldThrowDomainException()
    {
        //Arrange
        var payment = new PaymentDTO(200, 3, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.PayForContractAsync(payment, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_PaymentValueMoreThenContractValuePlusPaid_ShouldThrowDomainException()
    {
        //Arrange
        var payment = new PaymentDTO(12312412, 1, 1);

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.PayForContractAsync(payment, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_PaymentDataPassesValidation_ShouldReturnPaymentViewDTO()
    {
        //Arrange
        var payment = new PaymentDTO(200, 2, 2);

        //Assert and Act
        await Should.NotThrowAsync(_service.PayForContractAsync(payment, CancellationToken.None));
    }

    [Fact]
    public async Task PayForContractAsync_ContractInactive_ShouldReturnPaymentViewDTOAndCreateNewContract()
    {
        //Arrange
        var payment = new PaymentDTO(200, 4, 3);

        //Assert and Act
        await Should.NotThrowAsync(_service.PayForContractAsync(payment, CancellationToken.None));
    }
    
    [Fact]
    public async Task GetPayment_PaymentNotInDatabase_ShouldThrowDomainException()
    {
        //Arrange
        var paymentID = 12314;

        //Assert and Act
        await Should.ThrowAsync<DomainException>(_service.GetPaymentAsync(paymentID, CancellationToken.None));
    }

    [Fact]
    public async Task GetPayment_PaymentInDatabase_ShouldReturnPaymentViewDTO()
    {
        //Arrange
        var paymentID = 1;

        //Assert and Act
        await Should.NotThrowAsync(_service.GetPaymentAsync(paymentID, CancellationToken.None));
    }


}