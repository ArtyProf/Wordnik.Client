using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetEtymologiesAsyncTests
{
    [Fact]
    public async Task GetEtymologiesAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new string[]
        {
            "A representative form or pattern."
        });

        var request = new GetEtymologiesRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Etymologies}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetEtymologiesAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.First());
            });
    }

    [Fact]
    public async Task GetEtymologiesAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetEtymologiesRequest>(
            (client, req) => client.GetEtymologiesAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetEtymologiesAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetEtymologiesRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetEtymologiesAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetEtymologiesAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetEtymologiesRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Etymologies}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetEtymologiesAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetEtymologiesAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetEtymologiesRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetEtymologiesRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetEtymologiesAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetEtymologiesAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetEtymologiesRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetEtymologiesRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetEtymologiesAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
