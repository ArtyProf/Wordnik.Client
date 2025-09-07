using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents a single hyphenated fragment from the Wordnik API hyphenation response.
    /// </summary>
    public class HyphenationResponse
    {
        /// <summary>
        /// The text of the hyphenated fragment.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// The sequence number of the hyphenated fragment.
        /// </summary>
        [JsonProperty("seq")]
        public int Sequence { get; set; }

        /// <summary>
        /// The type of the hyphenation (e.g., "stress"). This field is optional.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
