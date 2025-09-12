using Wordnik.Client.Enums;
using Wordnik.Client.Requests;

namespace Wordnik.Client.UnitTests.Requests;

public class GetRandomWordsRequestTests
{
    [Theory]
    [InlineData(
        // Case 1: Default values
        true, null, null, null, null, 1, -1, 5, -1, SortByType.NotSet, SortOrderType.NotSet, 10,
        "hasDictionaryDef=true&minDictionaryCount=1&maxDictionaryCount=-1&minLength=5&maxLength=-1&limit=10"
    )]
    [InlineData(
        // Case 2: Include parts of speech and corpus count limits
        true, new PartOfSpeechType[] { PartOfSpeechType.Noun, PartOfSpeechType.Verb }, null, 100, 500, 2, 10, 4, 20, SortByType.NotSet, SortOrderType.NotSet, 15,
        "hasDictionaryDef=true&includePartOfSpeech=noun,verb&minCorpusCount=100&maxCorpusCount=500&minDictionaryCount=2&maxDictionaryCount=10&minLength=4&maxLength=20&limit=15"
    )]
    [InlineData(
        // Case 3: Exclude parts of speech with no corpus or dictionary limits
        false, null, new PartOfSpeechType[] { PartOfSpeechType.Adjective, PartOfSpeechType.Adverb }, null, null, null, null, null, null, SortByType.NotSet, SortOrderType.NotSet, 20,
        "hasDictionaryDef=false&excludePartOfSpeech=adjective,adverb&limit=20"
    )]
    [InlineData(
        // Case 4: Sort by count, descending order, and default limits
        true, null, null, null, null, 1, -1, 5, 10, SortByType.Count, SortOrderType.Descending, 25,
        "hasDictionaryDef=true&minDictionaryCount=1&maxDictionaryCount=-1&minLength=5&maxLength=10&sortBy=count&sortOrder=desc&limit=25"
    )]
    [InlineData(
        // Case 5: Sort alphabetically, ascending order, include parts of speech, and complex limits
        true, new PartOfSpeechType[] { PartOfSpeechType.ProperNoun, PartOfSpeechType.Interjection }, null, 20, 200, 3, 8, 6, 12, SortByType.Alphabetical, SortOrderType.Ascending, 30,
        "hasDictionaryDef=true&includePartOfSpeech=proper-noun,interjection&minCorpusCount=20&maxCorpusCount=200&minDictionaryCount=3&maxDictionaryCount=8&minLength=6&maxLength=12&sortBy=alpha&sortOrder=asc&limit=30"
    )]
    [InlineData(
        // Case 6: No limits and only sorting
        true, null, null, null, null, null, null, null, null, SortByType.Alphabetical, SortOrderType.Descending, 50,
        "hasDictionaryDef=true&sortBy=alpha&sortOrder=desc&limit=50"
    )]
    [InlineData(
        // Case 7: No parts of speech, no sorting, only corpus and dictionary limits
        true, null, null, 5, 10, 2, 4, null, null, SortByType.NotSet, SortOrderType.NotSet, 5,
        "hasDictionaryDef=true&minCorpusCount=5&maxCorpusCount=10&minDictionaryCount=2&maxDictionaryCount=4&limit=5"
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
        SortByType sortBy,
        SortOrderType sortOrder,
        int? limit,
        string expectedQueryString)
    {
        // Arrange
        var request = new GetRandomWordsRequest
        {
            HasDictionaryDef = hasDictionaryDef,
            IncludePartOfSpeech = includePartOfSpeech?.ToList(),
            ExcludePartOfSpeech = excludePartOfSpeech?.ToList(),
            MinCorpusCount = minCorpusCount,
            MaxCorpusCount = maxCorpusCount,
            MinDictionaryCount = minDictionaryCount,
            MaxDictionaryCount = maxDictionaryCount,
            MinLength = minLength,
            MaxLength = maxLength,
            SortBy = sortBy,
            SortOrder = sortOrder,
            Limit = limit
        };

        // Act
        var queryString = request.ToString();

        // Assert
        Assert.Equal(expectedQueryString, queryString);
    }
}