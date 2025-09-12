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
                Assert.True(pronunciation.Id.Length > 0);
            }
            Assert.True(pronunciation.Raw.Length > 0);
            Assert.True(pronunciation.RawType.Length > 0);
            Assert.True(pronunciation.AttributionText.Length > 0);
            Assert.True(pronunciation.AttributionUrl.Length > 0);
        }
    }
}
