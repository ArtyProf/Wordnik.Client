using System.Collections.Generic;
using System.Threading.Tasks;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client
{
    /// <summary>
    /// Defines the contract for a Wordnik API client.
    /// </summary>
    public interface IWordnikClient
    {
        /// <summary>
        /// Fetches definitions of a word from Wordnik API.
        /// </summary>
        /// <param name="request">Request model for retrieving definitions from the Wordnik API.</param>
        /// <returns>A list of definitions.</returns>
        Task<IEnumerable<DefinitionResponse>> GetDefinitionsAsync(GetDefinitionsRequest request);

        /// <summary>
        /// Retrieves examples for a specific word from the Wordnik API using the provided parameters.
        /// </summary>
        /// <param name="request">The request model containing query parameters for the API call.</param>
        /// <returns>A response object containing examples for the word.</returns>
        Task<ExamplesResponse> GetExamplesAsync(GetExamplesRequest request);

        /// <summary>
        /// Retrieves frequency data for a specific word from the Wordnik API.
        /// </summary>
        /// <param name="request">The request model containing query parameters for the API call.</param>
        /// <returns>A response object containing frequency data for the word.</returns>
        Task<FrequencyResponse> GetFrequencyAsync(GetFrequencyRequest request);

        /// <summary>
        /// Retrieves hyphenation data for a specific word from the Wordnik API.
        /// </summary>
        /// <param name="request">The request model containing query parameters for the API call.</param>
        /// <returns>A response object containing hyphenation data for the word.</returns>
        Task<IEnumerable<HyphenationResponse>> GetHyphenationAsync(GetHyphenationRequest request);
    }
}
