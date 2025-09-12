using Microsoft.Extensions.Configuration;
using System.Net;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected readonly string _apiKey;
    protected readonly HttpClient _httpClient;
    protected readonly IConfiguration _configuration;

    private const int MaxRetries = 3; // Maximum retry attempts
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

    /// <summary>
    /// Executes an asynchronous operation with retry logic in case of rate-limiting (HTTP 429).
    /// </summary>
    /// <typeparam name="T">The type of the task result.</typeparam>
    /// <param name="operation">The function to execute.</param>
    /// <returns>The result of the operation after retries (if applicable).</returns>
    protected async static Task<T> SendWithRetryAsync<T>(Func<Task<T>> operation)
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            try
            {
                return await operation();
            }
            catch
            {
                Console.WriteLine($"WARN: Received 429 Too Many Requests. Attempt {attempt} of {MaxRetries}.");

                if (attempt == MaxRetries)
                {
                    Console.WriteLine("ERROR: Maximum retry attempts reached. Throwing exception.");
                    throw;
                }

                await Task.Delay(RequestDelayInMilliseconds);
            }
        }

        throw new HttpRequestException("Failed after maximum retries.");
    }

    protected static async Task ThrottleAsync() => await Task.Delay(RequestDelayInMilliseconds);
}
