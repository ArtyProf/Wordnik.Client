using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class ExamplesIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 3)]
    public async Task GetExamplesAsync_WhenCalled_ShouldReturnExamples(string word, int limit)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetExamplesRequest
        {
            Word = word,
            Limit = limit
        };

        // Act
        await ThrottleAsync();
        var examples = await SendWithRetryAsync(() => client.GetExamplesAsync(request));

        // Assert
        Assert.NotNull(examples);
        Assert.NotEmpty(examples.Examples);
        Assert.True(examples.Examples.Count == limit);

        foreach (var example in examples.Examples)
        {
            Assert.False(string.IsNullOrWhiteSpace(example.Text), "Definition text is missing.");
            Assert.True(example.Url.Length > 0);
            Assert.True(example.Word == word);
            Assert.True(example.Title.Length > 0);
            Assert.NotNull(example.Year);
            Assert.NotNull(example.Provider?.Id);
            Assert.True(example.DocumentId > 0);
            Assert.True(example.Rating > 0);
            Assert.True(example.ExampleId > 0);
        }
    }
}
