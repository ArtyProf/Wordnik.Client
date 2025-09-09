using System;
using System.Collections.Generic;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents the request parameters for retrieving etymology data from the Wordnik API.
    /// </summary>
    public class GetEtymologiesRequest : IWord
    {
        /// <summary>
        /// The word to fetch etymology data for.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// If true, will return the canonical (root) form of the word.
        /// Default: false
        /// </summary>
        public bool? UseCanonical { get; set; }

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

            return string.Join("&", queryParams);
        }
    }
}
