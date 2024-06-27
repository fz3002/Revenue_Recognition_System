using Microsoft.Extensions.Configuration;
using Revenue_Recognition_System_Test.TestObjects;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Services;
using Shouldly;

namespace Revenue_Recognition_System_Test;

public class RevenueServiceTest
{
    private IRevenueService _service;

    public RevenueServiceTest()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _service = new RevenueService(new FakeContractRepository(), config);
    }

    [Fact]
    public async Task GetRevenueAsync_NoParameterGiven_ShouldReturnDecimalValueInPlnForAllProducts()
    {
        var result = await _service.GetRevenueAsync(-1, null, CancellationToken.None);
        result.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetRevenueAsync_ForGivenSoftware_ShouldReturnDecimalValueInPlnForAllProducts()
    {
        var result = await _service.GetRevenueAsync(1, null, CancellationToken.None);
        result.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetRevenueAsync_ForGivenCurrency_ShouldReturnDecimalValueInPlnForAllProducts()
    {
        var result = await _service.GetRevenueAsync(-1, "USD", CancellationToken.None);
        result.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetRevenueAsync_ForGivenCurrencyAndSoftware_ShouldReturnDecimalValueInPlnForAllProducts()
    {
        var result = await _service.GetRevenueAsync(123, "USD", CancellationToken.None);
        result.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetRevenueAsync_BadCurrencyCode_ShouldThrowDomainException()
    {
        var result = _service.GetRevenueAsync(-1, "asdfas", CancellationToken.None);
        await Should.ThrowAsync<DomainException>(result);
    }

    [Fact]
    public async Task GetRevenueAsync_BadCurrencyCodeAndGivenSoftware_ShouldThrowDomainException()
    {
        var result = _service.GetRevenueAsync(1, "asdfas", CancellationToken.None);
        await Should.ThrowAsync<DomainException>(result);
    }

    [Fact]
    public async Task GetExpectedRevenueAsync_ShouldReturnDecimalValueInPlnForAllProducts()
    {
        var result = await _service.GetExpectedRevenueAsync(CancellationToken.None);
        result.ShouldBeGreaterThanOrEqualTo(0);
    }
}