using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetDefinitionsAsyncTests
{
    [Fact]
    public async Task GetDefinitionsAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<DefinitionResponse>
        {
            new()
            {
                Text = "A representative form or pattern.",
                Word = "example",
                AttributionText = "From Wordnik",
                AttributionUrl = "https://www.wordnik.com",
                WordnikUrl = "https://www.wordnik.com/words/example",
                Citations =
                [
                    new() { Cite = "Oxford Dictionary", Source = "Book" }
                ],
                ExampleUses =
                [
                    new() { Text = "Example usage in a sentence." }
                ],
                ExtendedText = "An extended explanation of the word.",
                Labels =
                [
                    new() { Type = "region", Text = "US" }
                ],
                Notes =
                [
                    new() { Type = "editorial", Text = "Additional editorial note." }
                ],
                PartOfSpeech = "noun",
                RelatedWords =
                [
                    new() {
                        RelationshipType = "synonym",
                        Words = ["sample", "specimen"]
                    }
                ],
                Score = 100.0,
                SeqString = "1",
                Sequence = "1",
                SourceDictionary = "WordNet",
                TextProns =
                [
                    new() { Raw = "ig-zam-puhl", RawType = "IPA" }
                ]
            }
        });

        var request = new GetDefinitionsRequest { Word = "example", Limit = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Definitions}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetDefinitionsAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Single(response);

                var definition = response.First();
                Assert.Equal("A representative form or pattern.", definition.Text);
                Assert.Equal("example", definition.Word);
                Assert.Equal("From Wordnik", definition.AttributionText);
                Assert.Equal("https://www.wordnik.com", definition.AttributionUrl);
                Assert.Equal("https://www.wordnik.com/words/example", definition.WordnikUrl);
                Assert.Equal("An extended explanation of the word.", definition.ExtendedText);
                Assert.Equal("noun", definition.PartOfSpeech);
                Assert.Equal("WordNet", definition.SourceDictionary);
                Assert.Equal("1", definition.SeqString);
                Assert.Equal("1", definition.Sequence);
                Assert.Equal(100.0, definition.Score);

                Assert.NotNull(definition.Citations);
                Assert.NotEmpty(definition.Citations);
                var citation = definition.Citations.First();
                Assert.Equal("Oxford Dictionary", citation.Cite);
                Assert.Equal("Book", citation.Source);

                Assert.NotNull(definition.ExampleUses);
                Assert.NotEmpty(definition.ExampleUses);
                var exampleUse = definition.ExampleUses.First();
                Assert.Equal("Example usage in a sentence.", exampleUse.Text);

                Assert.NotNull(definition.Labels);
                Assert.NotEmpty(definition.Labels);
                var label = definition.Labels.First();
                Assert.Equal("region", label.Type);
                Assert.Equal("US", label.Text);

                Assert.NotNull(definition.Notes);
                Assert.NotEmpty(definition.Notes);
                var note = definition.Notes.First();
                Assert.Equal("editorial", note.Type);
                Assert.Equal("Additional editorial note.", note.Text);

                Assert.NotNull(definition.RelatedWords);
                Assert.NotEmpty(definition.RelatedWords);
                var relatedWord = definition.RelatedWords.First();
                Assert.Equal("synonym", relatedWord.RelationshipType);
                Assert.NotNull(relatedWord.Words);
                Assert.Equal(new List<string> { "sample", "specimen" }, relatedWord.Words);

                Assert.NotNull(definition.TextProns);
                Assert.NotEmpty(definition.TextProns);
                var textPron = definition.TextProns.First();
                Assert.Equal("ig-zam-puhl", textPron.Raw);
                Assert.Equal("IPA", textPron.RawType);
            });
    }

    [Fact]
    public async Task GetDefinitionsAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetDefinitionsRequest>(
            (client, req) => client.GetDefinitionsAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetDefinitionsAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetDefinitionsRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetDefinitionsAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetDefinitionsAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetDefinitionsRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Definitions}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetDefinitionsAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetDefinitionsAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetDefinitionsRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetDefinitionsRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetDefinitionsAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetDefinitionsAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetDefinitionsRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetDefinitionsRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetDefinitionsAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}