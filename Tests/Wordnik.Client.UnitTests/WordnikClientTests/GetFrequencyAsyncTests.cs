using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetFrequencyAsyncTests
{
    [Fact]
    public async Task GetFrequencyAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new FrequencyResponse
        {
            FrequenciesByYears =
            [
                new FrequencyYear
                {
                    Year = "1900",
                    Count = 1000
                }
            ]
        });

        var request = new GetFrequencyRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Frequency}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetFrequencyAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Equal("1900", response.FrequenciesByYears.First().Year);
            });
    }

    [Fact]
    public async Task GetFrequencyAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetFrequencyRequest>(
            (client, req) => client.GetFrequencyAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetFrequencyAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetFrequencyRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetFrequencyAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetFrequencyAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetFrequencyRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.Frequency}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetFrequencyAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetFrequencyAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetFrequencyRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetFrequencyRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetFrequencyAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetFrequencyAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetFrequencyRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetFrequencyRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetFrequencyAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
