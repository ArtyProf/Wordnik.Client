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
            new() { Text = "A representative form or pattern." }
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
                Assert.Equal("A representative form or pattern.", response.First().Text);
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