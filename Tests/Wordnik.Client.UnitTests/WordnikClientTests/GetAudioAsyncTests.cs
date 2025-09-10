using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetAudioAsyncTests
{
    [Fact]
    public async Task GetAudioAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<AudioResponse>
        {
            new() { Word = "A representative form or pattern." }
        });

        var request = new GetAudioRequest { Word = "example", Limit = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Audio}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetAudioAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Single(response);
                Assert.Equal("A representative form or pattern.", response.First().Word);
            });
    }

    [Fact]
    public async Task GetAudioAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetAudioRequest>(
            (client, req) => client.GetAudioAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetAudioAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetAudioRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetAudioAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetAudioAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetAudioRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Audio}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetAudioAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetAudioAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetAudioRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetAudioRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetAudioAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetAudioAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetAudioRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetAudioRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetAudioAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}