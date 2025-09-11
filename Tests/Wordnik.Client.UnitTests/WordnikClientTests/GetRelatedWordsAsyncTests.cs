using Newtonsoft.Json;
using System.Net;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client.UnitTests.WordnikClientTests;

public class GetRelatedWordsAsyncTests
{
    [Fact]
    public async Task GetRelatedWordsAsync_ShouldConstructCorrectUrlAndReturnData()
    {
        var responseContent = JsonConvert.SerializeObject(new List<RelatedWordsResponse>
        {
            new() { Words = ["A representative form or pattern."] }
        });

        var request = new GetRelatedWordsRequest { Word = "example", LimitPerRelationshipType = 1 };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.RelatedWords}?{request}";

        await WordnikTestHelper.RunGenericApiMethodTest(
            mockResponseContent: responseContent,
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRelatedWordsAsync(req),
            request: request,
            assertions: response =>
            {
                Assert.NotNull(response);
                Assert.Single(response);
                Assert.Equal("A representative form or pattern.", response.First().Words.First());
            });
    }

    [Fact]
    public async Task GetRelatedWordsAsync_RequestIsNull_ShouldThrowArgumentNullException()
    {
        await WordnikTestHelper.RunNullRequestTest<GetRelatedWordsRequest>(
            (client, req) => client.GetRelatedWordsAsync(req),
            "request");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetRelatedWordsAsync_RequestWordIsInvalid_ShouldThrowArgumentException(string word)
    {
        var request = new GetRelatedWordsRequest
        {
            Word = word
        };

        await WordnikTestHelper.RunInvalidWordValidationTest(
            (client, req) => client.GetRelatedWordsAsync(req),
            request,
            "Word cannot be null or empty.");
    }

    [Fact]
    public async Task GetRelatedWordsAsync_HttpResponseIsNotSuccess_ShouldThrowHttpRequestException()
    {
        var request = new GetRelatedWordsRequest { Word = "example" };
        var expectedUrl = $"{WordnikConstants.WordnikApiUrl}word.json/example/{WordnikConstants.RelatedWords}?{request}";

        await WordnikTestHelper.RunHttpFailureTest(
            statusCode: HttpStatusCode.BadRequest,
            reasonPhrase: "Bad Request",
            expectedUrl: expectedUrl,
            apiMethod: (client, req) => client.GetRelatedWordsAsync(req),
            request: request);
    }

    [Fact]
    public async Task GetRelatedWordsAsync_InvalidJsonResponse_ShouldThrowJsonSerializationException()
    {
        var malformedJson = "{ invalid json }";
        var request = new GetRelatedWordsRequest { Word = "example" };

        await WordnikTestHelper.RunMalformedJsonTest<GetRelatedWordsRequest, JsonReaderException>(
            malformedJson,
            (client, req) => client.GetRelatedWordsAsync(req),
            request,
            "Invalid character after parsing property"
        );
    }

    [Fact]
    public async Task GetRelatedWordsAsync_HttpClientThrowsException_ShouldPropagateException()
    {
        var request = new GetRelatedWordsRequest { Word = "example" };

        await WordnikTestHelper.RunHttpClientExceptionPropagationTest<GetRelatedWordsRequest, InvalidOperationException>(
            new InvalidOperationException("A test exception during HTTP processing"),
            (client, req) => client.GetRelatedWordsAsync(req),
            request,
            "A test exception during HTTP processing"
        );
    }
}
