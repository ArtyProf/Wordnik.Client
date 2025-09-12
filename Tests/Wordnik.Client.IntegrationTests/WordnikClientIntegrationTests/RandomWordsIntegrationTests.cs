using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class RandomWordsIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetRandomWords_WhenCalled_ShouldReturnRandomWords()
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetRandomWordsRequest
        {
            IncludePartOfSpeech =
            [
                PartOfSpeechType.Noun
            ],
            HasDictionaryDef = true,
            ExcludePartOfSpeech = 
            [
                PartOfSpeechType.Affix
            ],
            SortBy = SortByType.Alphabetical,
            SortOrder = SortOrderType.Descending,
            Limit = 10
        };

        // Act
        await ThrottleAsync();
        var randomWords = await SendWithRetryAsync(() => client.GetRandomWordsAsync(request));

        // Assert
        Assert.NotNull(randomWords);
        Assert.NotEmpty(randomWords);
        Assert.True(randomWords.Count() == request.Limit);

        foreach (var randomWord in randomWords)
        {
            Assert.True(randomWord.Word.Length > 0);
        }
    }
}
