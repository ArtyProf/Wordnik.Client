using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class RelatedWordsIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 3)]
    public async Task GetRelatedWords_WhenCalled_ShouldReturnDefinitions(string word, int limitPerRelationshipType)
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
        var relatedWords = await client.GetRelatedWordsAsync(request);

        // Assert
        Assert.NotNull(relatedWords);
        Assert.NotEmpty(relatedWords);

        foreach (var relatedWord in relatedWords)
        {
            Assert.True(relatedWord.Words.Count > 0 && relatedWord.Words.Count <= limitPerRelationshipType);
            Assert.True(relatedWord.Words.All(x => x.Length > 0));
            Assert.True(relatedWord.RelationshipType.Length > 0);
        }
    }
}
