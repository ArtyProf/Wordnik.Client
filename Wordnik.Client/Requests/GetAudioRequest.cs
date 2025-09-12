using System;
using System.Collections.Generic;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents a request to fetch audio metadata for a word using the Wordnik API.
    /// </summary>
    public class GetAudioRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word for which to fetch audio metadata.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets whether to return the canonical (root) form of the word (e.g., 'cats' → 'cat').
        /// Default: false.
        /// </summary>
        public bool? UseCanonical { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// </summary>
        public int Limit { get; set; }

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

            if (Limit > 0)
            {
                queryParams.Add($"limit={Limit}");
            }

            return string.Join("&", queryParams);
        }
    }
}
