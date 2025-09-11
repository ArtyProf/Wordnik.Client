using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetScrabbleScoreAsyncTests
{
    [Fact]
    public async Task GetScrabbleScoreAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new ScrabbleScoreResponse
        {
            Value = 10
        });

        var request = new GetScrabbleScoreRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.ScrabbleScore}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetScrabbleScoreAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal(10, response.Value);
            });
    }

    [Fact]
    public async Task GetScrabbleScoreAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetScrabbleScoreRequest>(
            (client, req) => client.GetScrabbleScoreAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetScrabbleScoreAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetScrabbleScoreRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetScrabbleScoreAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetScrabbleScoreAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetScrabbleScoreRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.ScrabbleScore}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetScrabbleScoreAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetScrabbleScoreAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetScrabbleScoreRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetScrabbleScoreRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetScrabbleScoreAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetScrabbleScoreAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetScrabbleScoreRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetScrabbleScoreRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetScrabbleScoreAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
