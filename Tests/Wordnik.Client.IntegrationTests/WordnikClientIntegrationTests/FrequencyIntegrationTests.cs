using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class FrequencyIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 2010, 2012)]
    public async Task GetFrequencyAsync_WhenCalled_ShouldReturnFrequency(string word, int startYear, int endYear)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetFrequencyRequest
        {
            Word = word,
            StartYear = startYear,
            EndYear = endYear
        };
        var yearsBetween = endYear - startYear + 1;

        // Act
        await ThrottleAsync();
        var frequencies = await SendWithRetryAsync(() => client.GetFrequencyAsync(request));

        // Assert
        Assert.NotNull(frequencies);
        Assert.NotEmpty(frequencies.FrequenciesByYears);
        Assert.Equal(yearsBetween, frequencies.FrequenciesByYears.Count);
        Assert.True(frequencies.Word == word);
        Assert.True(frequencies.TotalCount > 0, "TotalCount must be greater than zero.");
        Assert.True(frequencies.UnknownYearCount > 0, "UnknownYearCount must be greater than zero.");

        foreach (var frequency in frequencies.FrequenciesByYears)
        {
            Assert.False(string.IsNullOrWhiteSpace(frequency.Year), "Frequency Year must not be null or empty.");
            Assert.True(frequency.Count > 0, "Frequency Count must be greater than zero.");
        }
    }
}
