using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class TopResultIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example")]
    public async Task GetTopExample_WhenCalled_ShouldReturnTopExample(string word)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetTopExampleRequest
        {
            Word = word
        };

        // Act
        await ThrottleAsync();
        var topExample = await SendWithRetryAsync(() => client.GetTopExampleAsync(request));

        // Assert
        Assert.NotNull(topExample);
        Assert.Equal(word, topExample.Word);
        Assert.True(topExample.Year > 0, "Year must be greater than zero.");
        Assert.True(topExample.Rating > 0, "Rating must be greater than zero.");
        Assert.NotNull(topExample.Provider);
        Assert.True(topExample.Provider.Id > 0, "Provider.Id must be greater than zero.");
        Assert.False(string.IsNullOrWhiteSpace(topExample.Url), "URL must not be null or empty.");
        Assert.False(string.IsNullOrWhiteSpace(topExample.Text), "Text must not be null or empty.");
        Assert.False(string.IsNullOrWhiteSpace(topExample.Title), "Title must not be null or empty.");
        Assert.True(topExample.DocumentId > 0, "DocumentId must be greater than zero.");
        Assert.True(topExample.ExampleId > 0, "ExampleId must be greater than zero.");
    }
}
