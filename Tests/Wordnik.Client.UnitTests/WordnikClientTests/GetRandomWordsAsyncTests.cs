using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetRandomWordsAsyncTests
{
    [Fact]
    public async Task GetRandomWordsAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<RandomWordResponse>
        {
            new(){ Word = "A representative form or pattern." }
        });

        var request = new GetRandomWordsRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.RandomWords}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRandomWordsAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.First().Word);
            });
    }

    [Fact]
    public async Task GetRandomWordsAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetRandomWordsRequest>(
            (client, req) => client.GetRandomWordsAsync(req),
            "request");
    }

    [Fact]
    public async Task GetRandomWordsAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetRandomWordsRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.RandomWords}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRandomWordsAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetRandomWordsAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetRandomWordsRequest();

        await WordnikTestHelper.RunMalformedJsonTest<GetRandomWordsRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetRandomWordsAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetRandomWordsAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetRandomWordsRequest();

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetRandomWordsRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetRandomWordsAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
