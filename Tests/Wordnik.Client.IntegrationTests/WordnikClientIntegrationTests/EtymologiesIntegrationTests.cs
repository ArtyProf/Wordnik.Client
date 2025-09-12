using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class EtymologiesIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example")]
    public async Task GetEtymologies_WhenCalled_ShouldReturnEtymologies(string word)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetEtymologiesRequest
        {
            Word = word
        };

        // Act
        await ThrottleAsync();
        var etymologies = await SendWithRetryAsync(() => client.GetEtymologiesAsync(request));

        // Assert
        Assert.NotNull(etymologies);
        Assert.True(etymologies.First().Length > 0);
        Assert.Contains("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", etymologies.First());
    }
}
