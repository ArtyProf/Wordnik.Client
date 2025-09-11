using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents a text pronunciation for a word returned by the Wordnik API.
    /// </summary>
    public class PronunciationResponse
    {
        /// <summary>
        /// Gets or sets the raw pronunciation string.
        /// </summary>
        [JsonProperty("raw")]
        public string Raw { get; set; }

        /// <summary>
        /// Gets or sets the type of the raw pronunciation (e.g., "ahd-5", "IPA").
        /// </summary>
        [JsonProperty("rawType")]
        public string RawType { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the pronunciation.
        /// </summary>
        [JsonProperty("seq")]
        public int Seq { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the pronunciation (if available).
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the attribution text for the pronunciation.
        /// </summary>
        [JsonProperty("attributionText")]
        public string AttributionText { get; set; }

        /// <summary>
        /// Gets or sets the attribution URL for the pronunciation.
        /// </summary>
        [JsonProperty("attributionUrl")]
        public string AttributionUrl { get; set; }
    }
}
