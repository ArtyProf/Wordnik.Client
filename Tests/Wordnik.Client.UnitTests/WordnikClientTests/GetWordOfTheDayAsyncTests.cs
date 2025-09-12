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
            Word = "A representative form or pattern."
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
                Assert.Equal("A representative form or pattern.", response.Word);
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
