using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents the response for the Scrabble score of a word.
    /// </summary>
    public class ScrabbleScoreResponse
    {
        /// <summary>
        /// Gets or sets the Scrabble score for the word.
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
