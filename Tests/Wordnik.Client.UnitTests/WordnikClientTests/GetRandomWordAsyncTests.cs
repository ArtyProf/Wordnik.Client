using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetRandomWordAsyncTests
{
    [Fact]
    public async Task GetRandomWordAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new RandomWordResponse
        {
            Word = "A representative form or pattern."
        });

        var request = new GetRandomWordRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.RandomWord}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRandomWordAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.Word);
            });
    }

    [Fact]
    public async Task GetRandomWordAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetRandomWordRequest>(
            (client, req) => client.GetRandomWordAsync(req),
            "request");
    }

    [Fact]
    public async Task GetRandomWordAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetRandomWordRequest();
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}words.json/{WordnikConstants.RandomWord}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRandomWordAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetRandomWordAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetRandomWordRequest();

        await WordnikTestHelper.RunMalformedJsonTest<GetRandomWordRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetRandomWordAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetRandomWordAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetRandomWordRequest();

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetRandomWordRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetRandomWordAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
