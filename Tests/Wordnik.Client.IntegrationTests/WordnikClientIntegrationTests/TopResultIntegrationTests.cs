using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class TopResultIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example")]
    public async Task GetTopExample_WhenCalled_ShouldReturnDefinitions(string word)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetTopExampleRequest
        {
            Word = word
        };

        // Act
        await ThrottleAsync();
        var topExample = await client.GetTopExampleAsync(request);

        // Assert
        Assert.NotNull(topExample);
        Assert.True(topExample.Year > 0);
        Assert.True(topExample.Word == word);
        Assert.True(topExample.Rating > 0);
        Assert.True(topExample.Provider.Id > 0);
        Assert.True(topExample.Url.Length > 0);
        Assert.True(topExample.Text.Length > 0);
        Assert.True(topExample.Title.Length > 0);
        Assert.True(topExample.DocumentId > 0);
        Assert.True(topExample.ExampleId > 0);
    }
}
