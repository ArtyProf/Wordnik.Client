using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents the request model for the Wordnik API hyphenation endpoint.
    /// </summary>
    public class GetHyphenationRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word for which syllables or hyphenation details are requested.
        /// This is a <b>required</b> field.
        /// </summary>
        [JsonProperty("word")]
        public string Word { get; set; }

        /// <summary>
        /// If true, will return the root form of the word (e.g., 'cats' -> 'cat').
        /// Default is false.
        /// </summary>
        [JsonProperty("useCanonical")]
        public bool? UseCanonical { get; set; }

        /// <summary>
        /// Get results from a single dictionary. Valid options are:
        /// ahd-5, century, wiktionary, webster, wordnet.
        /// </summary>
        [JsonProperty("sourceDictionary")]
        public SourceDictionariesType SourceDictionary { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to retrieve.
        /// Default is 50.
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        /// <summary>
        /// Constructs the query string representation of all parameters for this request.
        /// </summary>
        public override string ToString()
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Word))
            {
                queryParams.Add($"word={Uri.EscapeDataString(Word)}");
            }

            if (UseCanonical.HasValue)
            {
                queryParams.Add($"useCanonical={UseCanonical.ToString().ToLower()}");
            }

            if (SourceDictionary != SourceDictionariesType.All)
            {
                queryParams.Add($"sourceDictionary={SourceDictionary.ToApiString()}");
            }

            if (Limit > 0)
            {
                queryParams.Add($"limit={Limit}");
            }

            return string.Join("&", queryParams);
        }
    }
}
