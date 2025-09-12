using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class WordOfTheDayIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetWordOfTheDay_WhenCalled_ShouldReturnWordOfTheDay()
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetWordOfTheDayRequest();

        // Act
        await ThrottleAsync();
        var wordOfTheDay = await SendWithRetryAsync(() => client.GetWordOfTheDayAsync(request));

        // Assert
        Assert.NotNull(wordOfTheDay);
        Assert.True(wordOfTheDay.ContentProvider.Id > 0, "ContentProvider.Id should be positive.");
        Assert.False(string.IsNullOrWhiteSpace(wordOfTheDay.ContentProvider.Name), "ContentProvider.Name should not be null or empty.");
        Assert.NotNull(wordOfTheDay.Definitions);
        Assert.NotEmpty(wordOfTheDay.Definitions);

        foreach (var definition in wordOfTheDay.Definitions)
        {
            Assert.False(string.IsNullOrWhiteSpace(definition.Text), "Definition text should not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(definition.Source), "Definition source should not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(definition.PartOfSpeech), "Definition part of speech should not be null or empty.");
        }

        Assert.True(wordOfTheDay.PublishDate > DateTime.MinValue, "PublishDate should be valid.");
        Assert.NotNull(wordOfTheDay.Examples);
        Assert.NotEmpty(wordOfTheDay.Examples);

        foreach (var example in wordOfTheDay.Examples)
        {
            Assert.NotNull(example.Text);
            Assert.True(example.Text.Length > 0, "Example text should not be empty.");
            Assert.False(string.IsNullOrWhiteSpace(example.Title), "Example title should not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(example.Url), "Example URL should not be null or empty.");
            Assert.True(example.Id > 0, "Example Id should be positive.");
        }
        Assert.NotNull(wordOfTheDay.PlainProductionDate);
        Assert.True(wordOfTheDay.PlainProductionDate.Length > 0, "PlainProductionDate should not be empty.");

        if (!string.IsNullOrWhiteSpace(wordOfTheDay.Note))
        {
            Assert.True(wordOfTheDay.Note.Length > 0, "Notes should not be empty.");
        }
    }
}
