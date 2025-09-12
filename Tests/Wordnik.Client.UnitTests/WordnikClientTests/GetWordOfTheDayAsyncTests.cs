using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetWordOfTheDayAsyncTests
{
    [Fact]
    public async Task GetWordOfTheDayAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new WordOfTheDayResponse
        {
            Id = "68899e2b924d55d89fb4c8f2",
            Word = "example",
            ContentProvider = new ContentProvider
            {
                Id = 711,
                Name = "Wordnik"
            },
            Definitions =
            [
                new() {
                    Source = "gcide",
                    Text = "A representative form or pattern.",
                    Note = "No additional notes.",
                    PartOfSpeech = "noun"
                }
            ],
                PublishDate = new DateTime(2025, 09, 01),
                Examples =
            [
                new() {
                    Url = "http://example.com/example",
                    Title = "Example Usage",
                    Text = "This is an example sentence for the word 'example'.",
                    Id = 123456789
                }
            ],
            PlainProductionDate = "2025-09-01",
            HtmlExtra = "<div>This is extra HTML content.</div>",
            Note = "Derived from Latin word exemplum."
        });

        var request = new GetWordOfTheDayRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.WordOfTheDay}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetWordOfTheDayAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("68899e2b924d55d89fb4c8f2", response.Id);
                Assert.Equal("example", response.Word);
                Assert.Equal(new DateTime(2025, 09, 01), response.PublishDate);
                Assert.Equal("2025-09-01", response.PlainProductionDate);
                Assert.Equal("<div>This is extra HTML content.</div>", response.HtmlExtra);
                Assert.Equal("Derived from Latin word exemplum.", response.Note);

                Assert.NotNull(response.ContentProvider);
                Assert.Equal(711, response.ContentProvider.Id);
                Assert.Equal("Wordnik", response.ContentProvider.Name);

                Assert.NotNull(response.Definitions);
                Assert.NotEmpty(response.Definitions);
                var definition = response.Definitions.First();
                Assert.Equal("gcide", definition.Source);
                Assert.Equal("A representative form or pattern.", definition.Text);
                Assert.Equal("No additional notes.", definition.Note);
                Assert.Equal("noun", definition.PartOfSpeech);

                Assert.NotNull(response.Examples);
                Assert.NotEmpty(response.Examples);
                var example = response.Examples.First();
                Assert.Equal("http://example.com/example", example.Url);
                Assert.Equal("Example Usage", example.Title);
                Assert.Equal("This is an example sentence for the word 'example'.", example.Text);
                Assert.Equal(123456789, example.Id);
            });
    }

    [Fact]
    public async Task GetWordOfTheDayAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetWordOfTheDayRequest>(
            (client, req) => client.GetWordOfTheDayAsync(req),
            "request");
    }

    [Fact]
    public async Task GetWordOfTheDayAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetWordOfTheDayRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.WordOfTheDay}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetWordOfTheDayAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetWordOfTheDayAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetWordOfTheDayRequest();

        await WordnikTestHelper.RunMalformedJsonTest<GetWordOfTheDayRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetWordOfTheDayAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetWordOfTheDayAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetWordOfTheDayRequest();

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetWordOfTheDayRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetWordOfTheDayAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
