using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class RelatedWordsIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 3)]
    public async Task GetRelatedWords_WhenCalled_ShouldReturnRelatedWords(string word, int limitPerRelationshipType)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetRelatedWordsRequest
        {
            Word = word,
            LimitPerRelationshipType = limitPerRelationshipType
        };

        // Act
        await ThrottleAsync();
        var relatedWords = await SendWithRetryAsync(() => client.GetRelatedWordsAsync(request));

        // Assert
        Assert.NotNull(relatedWords);
        Assert.NotEmpty(relatedWords);

        foreach (var relatedWord in relatedWords)
        {
            Assert.NotNull(relatedWord.Words);
            Assert.InRange(relatedWord.Words.Count, 1, limitPerRelationshipType);
            Assert.All(relatedWord.Words, word =>
                Assert.False(string.IsNullOrWhiteSpace(word), "Word in RelatedWord.Words must not be null or empty.")
            );
            Assert.False(string.IsNullOrWhiteSpace(relatedWord.RelationshipType), "RelatedWord.RelationshipType must not be null or empty.");
        }
    }
}
