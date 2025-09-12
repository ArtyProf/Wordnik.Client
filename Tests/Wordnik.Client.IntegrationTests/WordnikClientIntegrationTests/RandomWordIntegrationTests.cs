using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class RandomWordIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetRandomWord_WhenCalled_ShouldReturnRandomWord()
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetRandomWordRequest
        {
            IncludePartOfSpeech =
            [
                PartOfSpeechType.Noun
            ],
            HasDictionaryDef = true,
            ExcludePartOfSpeech = 
            [
                PartOfSpeechType.Affix
            ]
        };

        // Act
        await ThrottleAsync();
        var randomWord = await SendWithRetryAsync(() => client.GetRandomWordAsync(request));

        // Assert
        Assert.NotNull(randomWord);
        Assert.False(string.IsNullOrWhiteSpace(randomWord.Word), "Random word must not be null or empty.");
    }
}
