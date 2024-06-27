using Newtonsoft.Json.Linq;
using Revenue_Recognition_System.Exceptions;
using Revenue_Recognition_System.Repositories;

namespace Revenue_Recognition_System.Services;

public class RevenueService : IRevenueService
{
    private const string BaseCurrencyCode = "PLN";
    private readonly IContractRepository _contractRepository;
    private static readonly HttpClient Client = new HttpClient();
    private IConfiguration _configuration;

    public RevenueService(IContractRepository contractRepository, IConfiguration configuration)
    {
        _contractRepository = contractRepository;
        _configuration = configuration;
    }
    public async Task<decimal> GetRevenueAsync(int idSoftware, string? currency, CancellationToken cancellationToken)
    {
        var result = await _contractRepository.GetRevenueAsync(cancellationToken);
        if (result <= 0) return 0m;
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
        var apiKey = _configuration.GetValue<string>("ApiSettings:ExchangeRateApiKey");
        var url =
            $"http://api.exchangerate.host/convert?access_key={apiKey}&from={BaseCurrencyCode}&to={targetCurrency}&amount={amount}";

        var apiResponse = await Client.GetAsync(url);

        var responseBody = await apiResponse.Content.ReadAsStringAsync();

        var jsonResponse = JObject.Parse(responseBody);
        if ((bool)jsonResponse["success"] == true)
        {
            return (decimal)jsonResponse["result"];
        }
        throw new DomainException("Malformed currency code. Currency Code needs to conform to ISO4217");
    }
}