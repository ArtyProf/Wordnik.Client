using System;
using System.Collections.Generic;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents the request to fetch word relationship types from the API.
    /// </summary>
    public class GetRelatedWordsRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word for which to fetch relationship types.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// If true, will return the root form of the word (e.g., 'cats' -> 'cat').
        /// Default is false.
        /// </summary>
        public bool? UseCanonical { get; set; }

        /// <summary>
        /// Gets or sets a specific relationship type to filter by.
        /// </summary>
        public RelationshipType RelationshipTypes { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return for each relationship type.
        /// </summary>
        public int LimitPerRelationshipType { get; set; }

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

            if (RelationshipTypes != RelationshipType.All)
            {
                queryParams.Add($"relationshipTypes={RelationshipTypes.ToApiString()}");
            }

            if (LimitPerRelationshipType > 0)
            {
                queryParams.Add($"limitPerRelationshipType={LimitPerRelationshipType}");
            }

            return string.Join("&", queryParams);
        }
    }
}
