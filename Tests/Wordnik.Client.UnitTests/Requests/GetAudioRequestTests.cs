using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetAudioRequestTests
{
    [Theory]
    [InlineData(
           "apple", false, 0,
           "word=apple&useCanonical=false"
       )]
    [InlineData(
           "ball", true, 0,
           "word=ball&useCanonical=true"
       )]
    [InlineData(
           "car", null, 1,
           "word=car&limit=1"
       )]
    [InlineData(
           "car", false, 1,
           "word=car&useCanonical=false&limit=1"
       )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       bool? useCanonical,
       int limit,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetAudioRequest
        {
            Word = word,
            UseCanonical = useCanonical,
            Limit = limit
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
