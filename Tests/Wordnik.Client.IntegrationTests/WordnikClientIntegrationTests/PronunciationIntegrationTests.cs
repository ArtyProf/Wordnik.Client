using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class PronunciationIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 3)]
    public async Task GetPronunciations_WhenCalled_ShouldReturnPronunciations(string word, int limit)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetPronunciationRequest
        {
            Word = word,
            Limit = limit
        };

        // Act
        await ThrottleAsync();
        var pronunciations = await SendWithRetryAsync(() => client.GetPronunciationAsync(request));

        // Assert
        Assert.NotNull(pronunciations);
        Assert.NotEmpty(pronunciations);

        foreach (var pronunciation in pronunciations)
        {
            if (pronunciation.RawType == FormatType.Ahd5.ToApiString())
            {
                Assert.False(string.IsNullOrWhiteSpace(pronunciation.Id), "Pronunciation Id must not be null or empty.");
            }
            Assert.False(string.IsNullOrWhiteSpace(pronunciation.Raw), "Pronunciation Raw must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(pronunciation.RawType), "Pronunciation RawType must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(pronunciation.AttributionText), "Pronunciation AttributionText must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(pronunciation.AttributionUrl), "Pronunciation AttributionUrl must not be null or empty.");
        }
    }
}
