using Microsoft.AspNetCore.Mvc;
using Wordnik.Client;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly IWordnikClient _wordnikClient;

    private const string BadRequestResponse = "The request is required.";

    public WordsController(IWordnikClient wordnikClient)
    {
        _wordnikClient = wordnikClient ?? throw new ArgumentNullException(nameof(wordnikClient));
    }

    [HttpGet("audio")]
    public async Task<ActionResult<IEnumerable<AudioResponse>>> GetAudio([FromQuery] GetAudioRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var audio = await _wordnikClient.GetAudioAsync(request);

            return Ok(audio);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("definitions")]
    public async Task<ActionResult<IEnumerable<DefinitionResponse>>> GetDefinitions([FromQuery] GetDefinitionsRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var definitions = await _wordnikClient.GetDefinitionsAsync(request);

            return Ok(definitions);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("etymologies")]
    public async Task<ActionResult<string[]>> GetEtymologies([FromQuery] GetEtymologiesRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var definitions = await _wordnikClient.GetEtymologiesAsync(request);

            return Ok(definitions);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("examples")]
    public async Task<ActionResult<ExamplesResponse>> GetExamples([FromQuery] GetExamplesRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var definitions = await _wordnikClient.GetExamplesAsync(request);

            return Ok(definitions);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("frequency")]
    public async Task<ActionResult<FrequencyResponse>> GetFrequency([FromQuery] GetFrequencyRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var definitions = await _wordnikClient.GetFrequencyAsync(request);

            return Ok(definitions);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("hyphenation")]
    public async Task<ActionResult<IEnumerable<HyphenationResponse>>> GetHyphenation([FromQuery] GetHyphenationRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var definitions = await _wordnikClient.GetHyphenationAsync(request);

            return Ok(definitions);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("pronunciation")]
    public async Task<ActionResult<IEnumerable<PronunciationResponse>>> GetPronunciation([FromQuery] GetPronunciationRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var pronunciations = await _wordnikClient.GetPronunciationAsync(request);

            return Ok(pronunciations);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("relatedWords")]
    public async Task<ActionResult<IEnumerable<RelatedWordsResponse>>> GetRelatedWords([FromQuery] GetRelatedWordsRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var relatedWords = await _wordnikClient.GetRelatedWordsAsync(request);

            return Ok(relatedWords);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("scrabbleScore")]
    public async Task<ActionResult<ScrabbleScoreResponse>> GetScrabbleScore([FromQuery] GetScrabbleScoreRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var scrabbleScore = await _wordnikClient.GetScrabbleScoreAsync(request);

            return Ok(scrabbleScore);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("topExample")]
    public async Task<ActionResult<TopExampleResponse>> GetTopExample([FromQuery] GetTopExampleRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var topExample = await _wordnikClient.GetTopExampleAsync(request);

            return Ok(topExample);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("randomWord")]
    public async Task<ActionResult<RandomWordResponse>> GetRandomWord([FromQuery] GetRandomWordRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var randomWord = await _wordnikClient.GetRandomWordAsync(request);

            return Ok(randomWord);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("randomWords")]
    public async Task<ActionResult<IEnumerable<RandomWordResponse>>> GetRandomWords([FromQuery] GetRandomWordsRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var randomWords = await _wordnikClient.GetRandomWordsAsync(request);

            return Ok(randomWords);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpGet("wordOfTheDay")]
    public async Task<ActionResult<WordOfTheDayResponse>> GetWordOfTheDay([FromQuery] GetWordOfTheDayRequest request)
    {
        if (request == null)
        {
            return BadRequest(BadRequestResponse);
        }

        try
        {
            var wordOfTheDay = await _wordnikClient.GetWordOfTheDayAsync(request);

            return Ok(wordOfTheDay);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error communicating with Wordnik API: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}