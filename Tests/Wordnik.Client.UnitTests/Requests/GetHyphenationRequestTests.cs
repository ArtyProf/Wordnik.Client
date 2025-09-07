using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetHyphenationRequestTests
{
    [Theory]
    [InlineData(
           "apple", false, SourceDictionariesType.Ahd5, 10,
           "word=apple&useCanonical=false&sourceDictionary=ahd-5&limit=10"
       )]
    [InlineData(
           "ball", true, SourceDictionariesType.All, null,
           "word=ball&useCanonical=true"
       )]
    [InlineData(
           "car", null, SourceDictionariesType.All, null,
           "word=car"
       )]
    [InlineData(
           "data", null, SourceDictionariesType.All, 10,
           "word=data&limit=10"
       )]
    [InlineData(
           "example", true, SourceDictionariesType.Ahd5, null,
           "word=example&useCanonical=true&sourceDictionary=ahd-5"
       )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       bool? useCanonical,
       SourceDictionariesType sourceDictionary,
       int? limit,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetHyphenationRequest
        {
            Word = word,
            UseCanonical = useCanonical,
            SourceDictionary = sourceDictionary,
            Limit = limit
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
