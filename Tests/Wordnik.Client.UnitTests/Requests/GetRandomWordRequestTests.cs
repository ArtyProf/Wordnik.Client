using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetRandomWordRequestTests
{
    [Theory]
    [InlineData(
        // Case 1: Default values
        true, null, null, null, null, 1, -1, 5, -1,
        "hasDictionaryDef=true&minDictionaryCount=1&maxDictionaryCount=-1&minLength=5&maxLength=-1"
    )]
    [InlineData(
        // Case 2: Include parts of speech and limits
        true, new PartOfSpeechType[] { PartOfSpeechType.Noun, PartOfSpeechType.Verb }, null, 10, 100, null, null, 4, 10,
        "hasDictionaryDef=true&includePartOfSpeech=noun,verb&minCorpusCount=10&maxCorpusCount=100&minLength=4&maxLength=10"
    )]
    [InlineData(
        // Case 3: Exclude parts of speech and limits
        false, null, new PartOfSpeechType[] { PartOfSpeechType.Adjective, PartOfSpeechType.Adverb }, null, null, null, null, null, null,
        "hasDictionaryDef=false&excludePartOfSpeech=adjective,adverb"
    )]
    [InlineData(
        // Case 4: Complex set with corpus counts and dictionary counts
        true, new PartOfSpeechType[] { PartOfSpeechType.Affix, PartOfSpeechType.Article }, new PartOfSpeechType[] { PartOfSpeechType.Pronoun }, 50, 500, 2, 10, null, null,
        "hasDictionaryDef=true&includePartOfSpeech=affix,article&excludePartOfSpeech=pronoun&minCorpusCount=50&maxCorpusCount=500&minDictionaryCount=2&maxDictionaryCount=10"
    )]
    public void ToString_ShouldGenerateCorrectQueryString(
        bool hasDictionaryDef,
        PartOfSpeechType[] includePartOfSpeech,
        PartOfSpeechType[] excludePartOfSpeech,
        int? minCorpusCount,
        int? maxCorpusCount,
        int? minDictionaryCount,
        int? maxDictionaryCount,
        int? minLength,
        int? maxLength,
        string expectedQueryString)
    {
        // Arrange
        var request = new GetRandomWordRequest
        {
            HasDictionaryDef = hasDictionaryDef,
            IncludePartOfSpeech = includePartOfSpeech?.ToList(),
            ExcludePartOfSpeech = excludePartOfSpeech?.ToList(),
            MinCorpusCount = minCorpusCount,
            MaxCorpusCount = maxCorpusCount,
            MinDictionaryCount = minDictionaryCount,
            MaxDictionaryCount = maxDictionaryCount,
            MinLength = minLength,
            MaxLength = maxLength
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}