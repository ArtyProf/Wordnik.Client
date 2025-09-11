using System;
using System.Collections.Generic;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents a request to fetch text pronunciations for a word using the Wordnik API.
    /// </summary>
    public class GetPronunciationRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word for which to fetch text pronunciations.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets whether to return the canonical (root) form of the word (e.g., 'cats' → 'cat').
        /// Default: false.
        /// </summary>
        public bool? UseCanonical { get; set; }

        /// <summary>
        /// Gets or sets the source dictionary to use (e.g., "ahd-5", "century", "cmu", etc.).
        /// </summary>
        public SourceDictionariesType SourceDictionary { get; set; }

        /// <summary>
        /// Gets or sets the text pronunciation type (e.g., "ahd-5", "arpabet", "gcide-diacritical", "IPA").
        /// </summary>
        public FormatType TypeFormat { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// </summary>
        public int Limit { get; set; } = 50;

        /// <summary>
        /// Constructs a query string from the request parameters.
        /// </summary>
        /// <returns>A query string suitable for appending to the API URL.</returns>
        public override string ToString()
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(Word))
            {
                queryParams.Add($"word={Uri.EscapeDataString(Word)}");
            }

            if (UseCanonical.HasValue)
            {
                queryParams.Add($"useCanonical={UseCanonical.Value.ToString().ToLower()}");
            }

            if (SourceDictionary != SourceDictionariesType.All)
            {
                queryParams.Add($"sourceDictionary={SourceDictionary.ToApiString()}");
            }

            if (TypeFormat != FormatType.All)
            {
                queryParams.Add($"typeFormat={TypeFormat.ToApiString()}");
            }

            if (Limit > 0)
            {
                queryParams.Add($"limit={Limit}");
            }

            return string.Join("&", queryParams);
        }
    }
}
