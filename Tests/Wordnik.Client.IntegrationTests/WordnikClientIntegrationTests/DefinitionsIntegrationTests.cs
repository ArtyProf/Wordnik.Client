﻿using Wordnik.Client.Requests;

namespace Wordnik.Client.IntegrationTests.WordnikClientIntegrationTests;

public class DefinitionsIntegrationTests : IntegrationTestBase
{
    [Theory]
    [InlineData("example", 3)]
    public async Task GetDefinitions_WhenCalled_ShouldReturnDefinitions(string word, int limit)
    {
        // Arrange
        var client = new WordnikClient(_httpClient, _apiKey);

        var request = new GetDefinitionsRequest
        {
            Word = word,
            Limit = limit
        };

        // Act
        var definitions = await client.GetDefinitionsAsync(request);

        // Assert
        Assert.NotNull(definitions);
        Assert.NotEmpty(definitions);
        Assert.True(definitions.Count() == limit);

        foreach (var definition in definitions)
        {
            Assert.False(string.IsNullOrWhiteSpace(definition.Text), "Definition text is missing.");
            Assert.NotNull(definition.PartOfSpeech);
            Assert.NotNull(definition.Word);
            Assert.NotNull(definition.WordnikUrl);
        }
    }
}
