using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class AudioIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 2)]
    public async Task GetAudio_WhenCalled_ShouldReturnDefinitions(string word, int limit)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetAudioRequest
        {
            Word = word,
            Limit = limit
        };

        // Act
        await ThrottleAsync();
        var audioMetadatas = await client.GetAudioAsync(request);

        // Assert
        Assert.NotNull(audioMetadatas);
        Assert.NotEmpty(audioMetadatas);
        Assert.True(audioMetadatas.Count() == limit);

        foreach (var audioMetadata in audioMetadatas)
        {
            Assert.True(audioMetadata.Id > 0);
            Assert.True(audioMetadata.Word == word);
            Assert.True(audioMetadata.AudioType.Length > 0);
            Assert.True(audioMetadata.AttributionText.Length > 0);
            Assert.True(audioMetadata.AttributionUrl.Length > 0);
            Assert.True(audioMetadata.Duration > 0);
            Assert.True(audioMetadata.FileUrl.Length > 0);
            Assert.True(audioMetadata.CreatedBy.Length > 0);
        }
    }
}
