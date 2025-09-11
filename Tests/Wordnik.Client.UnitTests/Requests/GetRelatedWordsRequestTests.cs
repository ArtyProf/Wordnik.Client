using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetRelatedWordsRequestTests
{
    [Theory]
    [InlineData(
           "apple", false, RelationshipType.Variant, 10,
           "word=apple&useCanonical=false&relationshipTypes=variant&limitPerRelationshipType=10"
       )]
    [InlineData(
           "ball", true, RelationshipType.All, 0,
           "word=ball&useCanonical=true"
       )]
    [InlineData(
           "car", null, RelationshipType.All, 0,
           "word=car"
       )]
    [InlineData(
           "data", null, RelationshipType.All, 10,
           "word=data&limitPerRelationshipType=10"
       )]
    [InlineData(
           "example", true, RelationshipType.Form, 0,
           "word=example&useCanonical=true&relationshipTypes=form"
       )]
    public void ToString_ShouldGenerateCorrectQueryString(
       string word,
       bool? useCanonical,
       RelationshipType relationshipTypes,
       int limitPerRelationshipType,
       string expectedQueryString)
    {
        // Arrange
        var request = new GetRelatedWordsRequest
        {
            Word = word,
            UseCanonical = useCanonical,
            RelationshipTypes = relationshipTypes,
            LimitPerRelationshipType = limitPerRelationshipType
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}
