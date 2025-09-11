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
        var frequencies = await client.GetFrequencyAsync(request);

        // Assert
        Assert.NotNull(frequencies);
        Assert.NotEmpty(frequencies.FrequenciesByYears);
        Assert.Equal(yearsBetween, frequencies.FrequenciesByYears.Count);
        Assert.True(frequencies.Word == word);
        Assert.True(frequencies.TotalCount > 0);
        Assert.True(frequencies.UnknownYearCount > 0);

        foreach (var frequency in frequencies.FrequenciesByYears)
        {
            Assert.True(frequency.Year.Length > 0);
            Assert.True(frequency.Count > 0);
        }
    }
}
