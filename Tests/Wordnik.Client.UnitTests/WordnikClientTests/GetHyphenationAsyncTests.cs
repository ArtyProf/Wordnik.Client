using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetHyphenationAsyncTests
{
    [Fact]
    public async Task GetDefinitionsAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<HyphenationResponse>
        {
            new() { Text = "A representative form or pattern." }
        });

        var request = new GetHyphenationRequest { Word = "example", Limit = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Hyphenation}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetHyphenationAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Single(response);
                Assert.Equal("A representative form or pattern.", response.First().Text);
            });
    }

    [Fact]
    public async Task GetHyphenationAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetHyphenationRequest>(
            (client, req) => client.GetHyphenationAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetHyphenationAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetHyphenationRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetHyphenationAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetHyphenationAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetHyphenationRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Hyphenation}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetHyphenationAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetHyphenationAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetHyphenationRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetHyphenationRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetHyphenationAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetHyphenationAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetHyphenationRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetHyphenationRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetHyphenationAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
