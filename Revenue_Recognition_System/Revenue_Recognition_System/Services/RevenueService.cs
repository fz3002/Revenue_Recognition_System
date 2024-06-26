using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Revenue_Recognition_System.AppSettingsConfigurations;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class RevenueService : IRevenueService
{
    private const string BaseCurrencyCode = "PLN";
    private readonly IContractRepository _contractRepository;
    private readonly ApiSettings _apiSettings;
    private static readonly HttpClient Client = new HttpClient();

    public RevenueService(IContractRepository contractRepository,IOptions<ApiSettings> apiSettings)
    {
        _contractRepository = contractRepository;
        _apiSettings = apiSettings.Value;
    }
    public async Task<decimal> GetRevenueAsync(int idSoftware, string? currency, CancellationToken cancellationToken)
    {
        var result = await _contractRepository.GetRevenueAsync(cancellationToken);
        if (idSoftware > 0)
        {
            result =  await _contractRepository.GetRevenueAsync(idSoftware, cancellationToken);
        }

        if (currency != null)
        {
            return await ConvertToCurrency(currency, result);
        }

        return result;
    }

    public async Task<decimal> GetExpectedRevenueAsync(CancellationToken cancellationToken)
    {
        return await _contractRepository.GetExpectedRevenue(cancellationToken);
    }

    private async Task<decimal> ConvertToCurrency(string targetCurrency, decimal amount)
    {
        var url =
            $"https://api.exchangerate.host/convert?access_key={_apiSettings.ExchangeRateApiKey}&from={BaseCurrencyCode}&to={targetCurrency}&amount={amount}";

        var apiResponse = await Client.GetAsync(url);

        var responseBody = await apiResponse.Content.ReadAsStringAsync();

        var jsonResponse = JObject.Parse(responseBody);
        if (jsonResponse["result"].ToString() == "success")
        {
            return (decimal)jsonResponse["result"];
        }
        throw new DomainException("Malformed currency code. Currency Code needs to conform to ISO4217");
    }
}