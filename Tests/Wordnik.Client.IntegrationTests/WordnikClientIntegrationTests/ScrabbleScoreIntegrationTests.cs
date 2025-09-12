using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class ScrabbleScoreIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example")]
    public async Task GetScrabbleScore_WhenCalled_ShouldReturnScrabbleScore(string word)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetScrabbleScoreRequest
        {
            Word = word
        };

        // Act
        await ThrottleAsync();
        var scrabbleScore = await SendWithRetryAsync(() => client.GetScrabbleScoreAsync(request));

        // Assert
        Assert.NotNull(scrabbleScore);
        Assert.True(scrabbleScore.Value > 0, "ScrabbleScore.Value must be greater than zero.");
    }
}
