using Newtonsoft.Json;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetPronunciationAsyncTests
{
    [Fact]
    public async Task GetPronunciationAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<PronunciationResponse>
        {
            new()
            {
                Raw = "A representative form or pattern."
            }
        });

        var request = new GetPronunciationRequest { Word = "example", Limit = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Pronunciation}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetPronunciationAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("A representative form or pattern.", response.First().Raw);
            });
    }

    [Fact]
    public async Task GetPronunciationAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetPronunciationRequest>(
            (client, req) => client.GetPronunciationAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetPronunciationAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetPronunciationRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetPronunciationAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetPronunciationAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
        {
            var request = new GetPronunciationRequest { Word = "example" };
            var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Pronunciation}?{request}";

            await WordnikTestHelper.RunHttpFailureTest(
                statusCode: System.Net.HttpStatusCode.BadRequest,
                reasonPhrase: "Bad Request",
                expectedUrl: expectedUrl,
                apiMethod: (client, req) => client.GetPronunciationAsync(req),
                request: request);
        }

    [Fact]
    public async Task GetPronunciationAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetPronunciationRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetPronunciationRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetPronunciationAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
public async Task GetPronunciationAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetPronunciationRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetPronunciationRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetPronunciationAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
