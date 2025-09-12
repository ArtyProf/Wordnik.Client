using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class HyphenationIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example")]
    public async Task GetHyphenation_WhenCalled_ShouldReturnHyphenation(string word)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetHyphenationRequest
        {
            Word = word
        };

        // Act
        await ThrottleAsync();
        var hyphenationFragments = await SendWithRetryAsync(() => client.GetHyphenationAsync(request));

        // Assert
        Assert.NotNull(hyphenationFragments);
        Assert.NotEmpty(hyphenationFragments);
        Assert.Equal(0, hyphenationFragments.First().Sequence);

        foreach (var hyphenation in hyphenationFragments)
        {
            Assert.True(hyphenation.Text.Length > 0);

            if (hyphenation.Text != hyphenationFragments.First().Text)
            {
                Assert.True(hyphenation.Sequence > 0);
            }
        }
    }
}
