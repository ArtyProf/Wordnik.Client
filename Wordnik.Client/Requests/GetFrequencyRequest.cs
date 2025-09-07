using System;
using System.Collections.Generic;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents the request parameters for retrieving frequency data from the Wordnik API.
    /// </summary>
    public class GetFrequencyRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word to retrieve frequency data for.
        /// This is a <b>required</b> parameter.
        /// </summary>
        /// <example>example</example>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets whether to use the canonical form of the word (e.g., transform 'cats' to 'cat').
        /// If <c>true</c>, the canonical form of the word will be returned.
        /// If <c>false</c>, the exact word as provided will be used.
        /// Default is <c>false</c>.
        /// </summary>
        /// <remarks>
        /// Canonicalization ensures queries are mapped to the root word, such as 'cats' -> 'cat'.
        /// </remarks>
        /// <example>false</example>
        public bool? UseCanonical { get; set; }

        /// <summary>
        /// Gets or sets the starting year for frequency data retrieval.
        /// This specifies the earliest year to include in the frequency data.
        /// Default is <c>1800</c>.
        /// </summary>
        /// <example>1800</example>
        public int? StartYear { get; set; }

        /// <summary>
        /// Gets or sets the ending year for frequency data retrieval.
        /// This specifies the latest year to include in the frequency data.
        /// Default is <c>2012</c>.
        /// </summary>
        /// <example>2012</example>
        public int? EndYear { get; set; }

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

            if (StartYear.HasValue)
            {
                queryParams.Add($"startYear={StartYear.Value}");
            }

            if (EndYear.HasValue)
            {
                queryParams.Add($"endYear={EndYear.Value}");
            }

            return string.Join("&", queryParams);
        }
    }
}