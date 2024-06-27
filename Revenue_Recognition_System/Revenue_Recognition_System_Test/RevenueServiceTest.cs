using Microsoft.Extensions.Configuration;
using Revenue_Recognition_System_Test.TestObjects;
using Revenue_Recognition_System.Services;
using Shouldly;

namespace Revenue_Recognition_System_Test;

public class RevenueServiceTest
{
    private IRevenueService _service;

    public RevenueServiceTest()
    {
        _service = new RevenueService(new FakeContractRepository(), new ConfigurationBuilder().Build());
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
}