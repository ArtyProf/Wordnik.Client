using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetTopExampleRequestTests
{
    [Theory]
    [InlineData(
           "apple", false,
           "word=apple&useCanonical=false"
       )]
    [InlineData(
           "ball", true,
           "word=ball&useCanonical=true"
       )]
    [InlineData(
           "car", null,
           "word=car"
       )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       bool? useCanonical,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetTopExampleRequest
        {
            Word = word,
            UseCanonical = useCanonical
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
