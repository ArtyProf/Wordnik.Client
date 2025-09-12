using System.Collections.Generic;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Request model for retrieving random words from the Wordnik API.
    /// </summary>
    public class GetRandomWordsRequest : RandomWordBaseRequest
    {
        /// <summary>
        /// Attribute by which to sort the results.
        /// Options are "alpha" (alphabetical) or "count" (by frequency).
        /// </summary>
        public SortByType SortBy { get; set; }

        /// <summary>
        /// The sort order for the results.
        /// Options are "asc" (ascending) or "desc" (descending).
        /// </summary>
        public SortOrderType SortOrder { get; set; }

        /// <summary>
        /// Maximum number of results to return.
        /// Default value is 10.
        /// </summary>
        public int? Limit { get; set; } = 10;

        /// <inheritdoc />
        public override string ToString()
        {
            var baseQueryString = GenerateBaseQueryString();
            var queryParameters = new List<string> { baseQueryString };

            if (SortBy != SortByType.NotSet)
            {
                queryParameters.Add($"sortBy={SortBy.ToApiString()}");
            }

            if (SortOrder != SortOrderType.NotSet)
            {
                queryParameters.Add($"sortOrder={SortOrder.ToApiString()}");
            }

            if (Limit.HasValue)
            {
                queryParameters.Add($"limit={Limit.Value}");
            }

            return string.Join("&", queryParameters);
        }
    }
}