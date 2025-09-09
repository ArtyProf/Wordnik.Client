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

    public WordsController(IWordnikClient wordnikClient)
    {
        _wordnikClient = wordnikClient ?? throw new ArgumentNullException(nameof(wordnikClient));
    }

    [HttpGet("definitions")]
    public async Task<ActionResult<IEnumerable<DefinitionResponse>>> GetDefinitions([FromQuery] GetDefinitionsRequest request)
    {
        if (request == null)
        {
            return BadRequest("The request is required.");
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
            return BadRequest("The request is required.");
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
            return BadRequest("The request is required.");
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
            return BadRequest("The request is required.");
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
            return BadRequest("The request is required.");
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
}