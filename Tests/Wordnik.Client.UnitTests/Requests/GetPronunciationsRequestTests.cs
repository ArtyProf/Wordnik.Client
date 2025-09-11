using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetPronunciationRequestTests
{
    [Theory]
    [InlineData(
       "apple", false, SourceDictionariesType.Ahd5, FormatType.Ahd5, 10,
       "word=apple&useCanonical=false&sourceDictionary=ahd-5&typeFormat=ahd-5&limit=10"
        )]
    [InlineData(
       "ball", true, SourceDictionariesType.All, FormatType.All, 0,
       "word=ball&useCanonical=true"
        )]
    [InlineData(
       "car", null, SourceDictionariesType.All, FormatType.All, 0,
       "word=car"
        )]
    [InlineData(
       "data", null, SourceDictionariesType.All, FormatType.Ahd5, 10,
       "word=data&typeFormat=ahd-5&limit=10"
        )]
    [InlineData(
       "example", true, SourceDictionariesType.Ahd5, FormatType.All, 0,
       "word=example&useCanonical=true&sourceDictionary=ahd-5"
        )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       bool? useCanonical,
       SourceDictionariesType sourceDictionary,
       FormatType typeFormat,
       int limit,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetPronunciationRequest
        {
            Word = word,
            UseCanonical = useCanonical,
            SourceDictionary = sourceDictionary,
            TypeFormat = typeFormat,
            Limit = limit
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
