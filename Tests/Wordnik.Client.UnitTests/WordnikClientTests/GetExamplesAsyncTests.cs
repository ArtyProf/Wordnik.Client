using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetExamplesAsyncTests
{
    [Fact]
    public async Task GetExamplesAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new ExamplesResponse
        {
            Examples =
            [
                new Example
                {
                    Text = "A representative form or pattern."
                }
            ]
        });

        var request = new GetExamplesRequest { Word = "example", Limit = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Examples}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetExamplesAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.Examples.First().Text);
            });
    }

    [Fact]
    public async Task GetExamplesAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetExamplesRequest>(
            (client, req) => client.GetExamplesAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetExamplesAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetExamplesRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetExamplesAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetExamplesAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetExamplesRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Examples}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetExamplesAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetExamplesAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetExamplesRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetExamplesRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetExamplesAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetExamplesAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetExamplesRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetExamplesRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetExamplesAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
