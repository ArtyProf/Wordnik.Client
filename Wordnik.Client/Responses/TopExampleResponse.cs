using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents the response from the Top Example API for a given word.
    /// </summary>
    public class TopExampleResponse
    {
        /// <summary>
        /// Gets or sets the provider information for the top example response.
        /// </summary>
        [JsonProperty("provider")]
        public ProviderInfo Provider { get; set; } = new ProviderInfo();

        /// <summary>
        /// Gets or sets the year associated with the example usage.
        /// </summary>
        /// <remarks>
        /// Indicates when the example text was used or published.
        /// </remarks>
        [JsonProperty("year")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the rating assigned to this example.
        /// </summary>
        /// <remarks>
        /// Represents the level of relevance or importance of the example.
        /// </remarks>
        [JsonProperty("rating")]
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the URL to the source of the example text.
        /// </summary>
        /// <remarks>
        /// This is typically a reference to an external document, archive, or online resource.
        /// </remarks>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the word for which the example text applies.
        /// </summary>
        /// <remarks>
        /// This word corresponds to the query word being analyzed.
        /// </remarks>
        [JsonProperty("word")]
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the example text.
        /// </summary>
        /// <remarks>
        /// Contains the usage of the word in context, generally to demonstrate meaning or usage in text.
        /// </remarks>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <remarks>
        /// Refers to the document where the example originates.
        /// </remarks>
        [JsonProperty("documentId")]
        public long DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the example text.
        /// </summary>
        /// <remarks>
        /// Provides a unique ID for the example text to differentiate it from other examples.
        /// </remarks>
        [JsonProperty("exampleId")]
        public long ExampleId { get; set; }

        /// <summary>
        /// Gets or sets the title of the document containing the example.
        /// </summary>
        /// <remarks>
        /// Describes the title of the document where the example appears.
        /// </remarks>
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    /// <summary>
    /// Represents the provider information in the response.
    /// </summary>
    public class ProviderInfo
    {
        /// <summary>
        /// Gets or sets the identifier for the provider.
        /// </summary>
        /// <remarks>
        /// The provider ID uniquely identifies the source system providing the example data.
        /// </remarks>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}