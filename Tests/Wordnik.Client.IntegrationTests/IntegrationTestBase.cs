using Microsoft.Extensions.Configuration;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected readonly string _apiKey;
    protected readonly HttpClient _httpClient;
    protected readonly IConfiguration _configuration;

    private const int RequestDelayInMilliseconds = 5000; // 5 second delay

    protected IntegrationTestBase()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        _apiKey = _configuration["Wordnik:ApiKey"]
                 ?? throw new InvalidOperationException("API Key is missing from configuration.");

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(WordnikConstants.WordnikApiUrl)
        };
    }

    protected static async Task ThrottleAsync() => await Task.Delay(RequestDelayInMilliseconds);
}
