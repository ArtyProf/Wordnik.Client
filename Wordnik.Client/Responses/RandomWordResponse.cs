using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents the response for fetching a random word from the Wordnik API.
    /// </summary>
    public class RandomWordResponse
    {
        /// <summary>
        /// Gets or sets the random word.
        /// </summary>
        /// <remarks>
        /// The random word returned from the Wordnik API.
        /// Example: "building".
        /// </remarks>
        [JsonProperty("word")]
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the random word.
        /// </summary>
        /// <remarks>
        /// The unique ID corresponding to the random word (if applicable).
        /// Example: 0 (or another numeric ID).
        /// </remarks>
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
