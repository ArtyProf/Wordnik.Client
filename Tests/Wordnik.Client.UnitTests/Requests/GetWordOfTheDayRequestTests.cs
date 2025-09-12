using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetWordOfTheDayRequestTests
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("2025-09-01", "date=2025-09-01")]
    [InlineData("2000-01-01", "date=2000-01-01")]
    public void ToString_ShouldGenerateCorrectQueryString(string dateString, string expectedQueryString)
    {
        // Arrange
        DateTime? date = null;
        if (!string.IsNullOrEmpty(dateString))
        {
            date = DateTime.Parse(dateString);
        }
        var request = new GetWordOfTheDayRequest
        {
            Date = date
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
