using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetScrabbleScoreRequestTests
{
    [Theory]
    [InlineData(
           "apple",
           "word=apple"
       )]
    [InlineData(
           "ball",
           "word=ball"
       )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetScrabbleScoreRequest
        {
            Word = word
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
