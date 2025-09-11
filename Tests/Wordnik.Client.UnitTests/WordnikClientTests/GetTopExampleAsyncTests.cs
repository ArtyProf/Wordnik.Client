using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetTopExampleAsyncTests
{
    [Fact]
    public async Task GetTopExampleAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new TopExampleResponse
        {
            Word = "A representative form or pattern."
        });

        var request = new GetTopExampleRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.TopExample}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetTopExampleAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.Word);
            });
    }

    [Fact]
    public async Task GetTopExampleAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetTopExampleRequest>(
            (client, req) => client.GetTopExampleAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetTopExampleAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetTopExampleRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetTopExampleAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetTopExampleAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetTopExampleRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.TopExample}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetTopExampleAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetTopExampleAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetTopExampleRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetTopExampleRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetTopExampleAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetTopExampleAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetTopExampleRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetTopExampleRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetTopExampleAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
