using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Wordnik.Client.Helpers;
using Wordnik.Client.Requests;
using Wordnik.Client.Responses;

namespace Wordnik.Client
{
    /// <summary>
    /// A client for interacting with the Wordnik API, providing methods to word-related operations.
    /// </summary>
    public class WordnikClient : IWordnikClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordnikClient"/> class.
        /// Use this constructor for Dependency Injection (DI).
        /// </summary>
        /// <param name="httpClient">An instance of HttpClient provided by Dependency Injection.</param>
        /// <param name="apiKey">Your Wordnik API key.</param>
        public WordnikClient(HttpClient httpClient, string apiKey)
        {  
            _httpClient = httpClient;

            _httpClient.BaseAddress ??= new Uri(WordnikConstants.WordnikApiUrl);
            if (!_httpClient.DefaultRequestHeaders.Contains(WordnikConstants.WordnikApiKeyName))
            {
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
                }

                _httpClient.DefaultRequestHeaders.Add(WordnikConstants.WordnikApiKeyName, apiKey);
            }
        }

        /// <summary>
        /// Constructs and validates the request, sends an HTTP GET request, and returns the deserialized response.
        /// Includes validation for requests implementing <see cref="IWord"/> to ensure the `Word` property is not null or empty.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request implementing <see cref="IWord"/>, if applicable.</typeparam>
        /// <typeparam name="TResponse">The type to deserialize the response into.</typeparam>
        /// <param name="apiPath">The relative URL path template.</param>
        /// <param name="request">The request object containing query parameter and path placeholder values.</param>
        /// <returns>A <see cref="Task"/> containing the deserialized HTTP response.</returns>
        private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string apiPath, TRequest request)
            where TRequest : IWord
        {
            if (EqualityComparer<TRequest>.Default.Equals(request, default))
            {
                throw new ArgumentNullException(nameof(request), "Request object cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.Word))
            {
                throw new ArgumentException("Word cannot be null or empty.", nameof(request));
            }

            var url = $"word.json/{Uri.EscapeDataString(request.Word)}/{apiPath}?{request}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AudioResponse>> GetAudioAsync(GetAudioRequest request)
        {
            return await SendRequestAsync<GetAudioRequest, IEnumerable<AudioResponse>>(
                WordnikConstants.Audio,
                request
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DefinitionResponse>> GetDefinitionsAsync(GetDefinitionsRequest request)
        {
            return await SendRequestAsync<GetDefinitionsRequest, IEnumerable<DefinitionResponse>>(
                WordnikConstants.Definitions,
                request
            );
        }

        /// <inheritdoc />
        public async Task<string[]> GetEtymologiesAsync(GetEtymologiesRequest request)
        {
            return await SendRequestAsync<GetEtymologiesRequest, string[]>(
                WordnikConstants.Etymologies,
                request
            );
        }

        /// <inheritdoc />
        public async Task<ExamplesResponse> GetExamplesAsync(GetExamplesRequest request)
        {
            return await SendRequestAsync<GetExamplesRequest, ExamplesResponse>(
                WordnikConstants.Examples,
                request
            );
        }

        /// <inheritdoc />
        public async Task<FrequencyResponse> GetFrequencyAsync(GetFrequencyRequest request)
        {
            return await SendRequestAsync<GetFrequencyRequest, FrequencyResponse>(
                WordnikConstants.Frequency,
                request
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<HyphenationResponse>> GetHyphenationAsync(GetHyphenationRequest request)
        {
            return await SendRequestAsync<GetHyphenationRequest,  IEnumerable<HyphenationResponse>>(
                WordnikConstants.Hyphenation,
                request
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<PronunciationResponse>> GetPronunciationAsync(GetPronunciationRequest request)
        {
            return await SendRequestAsync<GetPronunciationRequest, IEnumerable<PronunciationResponse>>(
                WordnikConstants.Pronunciation,
                request
            );
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RelatedWordsResponse>> GetRelatedWordsAsync(GetRelatedWordsRequest request)
        {
            return await SendRequestAsync<GetRelatedWordsRequest, IEnumerable<RelatedWordsResponse>>(
                WordnikConstants.RelatedWords,
                request
            );
        }

        /// <inheritdoc />
        public async Task<ScrabbleScoreResponse> GetScrabbleScoreAsync(GetScrabbleScoreRequest request)
        {
            return await SendRequestAsync<GetScrabbleScoreRequest, ScrabbleScoreResponse>(
                WordnikConstants.ScrabbleScore,
                request
            );
        }

        /// <inheritdoc />
        public async Task<TopExampleResponse> GetTopExampleAsync(GetTopExampleRequest request)
        {
            return await SendRequestAsync<GetTopExampleRequest, TopExampleResponse>(
                WordnikConstants.TopExample,
                request
            );
        }
    }
}
