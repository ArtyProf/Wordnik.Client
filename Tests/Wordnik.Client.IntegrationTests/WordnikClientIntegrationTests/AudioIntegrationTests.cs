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
        var audioMetadatas = await SendWithRetryAsync(() => client.GetAudioAsync(request));

        // Assert
        Assert.NotNull(audioMetadatas);
        Assert.NotEmpty(audioMetadatas);
        Assert.True(audioMetadatas.Count() == limit);

        foreach (var audioMetadata in audioMetadatas)
        {
            Assert.True(audioMetadata.Id > 0, "Audio Id must be greater than zero.");
            Assert.Equal(word, audioMetadata.Word);
            Assert.False(string.IsNullOrWhiteSpace(audioMetadata.AudioType), "AudioType must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(audioMetadata.AttributionText), "AttributionText must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(audioMetadata.AttributionUrl), "AttributionUrl must not be null or empty.");
            Assert.True(audioMetadata.Duration > 0, "Duration must be greater than zero.");
            Assert.False(string.IsNullOrWhiteSpace(audioMetadata.FileUrl), "FileUrl must not be null or empty.");
            Assert.False(string.IsNullOrWhiteSpace(audioMetadata.CreatedBy), "CreatedBy must not be null or empty.");
        }
    }
}
