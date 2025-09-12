using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents the response from the Word of the Day API.
    /// </summary>
    public class WordOfTheDayResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Word of the Day entry.
        /// </summary>
        [JsonProperty("_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the word of the day.
        /// </summary>
        [JsonProperty("word")]
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the content provider information.
        /// </summary>
        [JsonProperty("contentProvider")]
        public ContentProvider ContentProvider { get; set; }

        /// <summary>
        /// Gets or sets the definitions for the word.
        /// </summary>
        [JsonProperty("definitions")]
        public List<Definition> Definitions { get; set; }

        /// <summary>
        /// Gets or sets the publication date for the word of the day.
        /// </summary>
        [JsonProperty("publishDate")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the list of example sentences for the word.
        /// </summary>
        [JsonProperty("examples")]
        public List<WordOfTheDayExample> Examples { get; set; }

        /// <summary>
        /// Gets or sets the plain production date (pdd).
        /// </summary>
        [JsonProperty("pdd")]
        public string PlainProductionDate { get; set; }

        /// <summary>
        /// Gets or sets any additional HTML content, if applicable.
        /// </summary>
        [JsonProperty("htmlExtra")]
        public string HtmlExtra { get; set; }

        /// <summary>
        /// Gets or sets any additional notes about the word.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }
    }

    /// <summary>
    /// Represents the content provider information for the word of the day.
    /// </summary>
    public class ContentProvider
    {
        /// <summary>
        /// Gets or sets the name of the content provider.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the content provider.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    /// <summary>
    /// Represents a single definition of the word.
    /// </summary>
    public class Definition
    {
        /// <summary>
        /// Gets or sets the source of the definition.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the definition text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets any additional notes associated with the definition.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the part of speech for the word.
        /// </summary>
        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }
    }

    /// <summary>
    /// Represents an example sentence used to demonstrate the word.
    /// </summary>
    public class WordOfTheDayExample
    {
        /// <summary>
        /// Gets or sets the URL to the source document containing the example.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title of the document containing the example.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the example text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the example text.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}