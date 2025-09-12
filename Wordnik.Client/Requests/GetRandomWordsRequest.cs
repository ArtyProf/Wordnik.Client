using System.Collections.Generic;
using System.Linq;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Request model for retrieving random words from the Wordnik API.
    /// </summary>
    public class GetRandomWordsRequest
    {
        /// <summary>
        /// Whether to only return words with dictionary definitions.
        /// Default value is true.
        /// </summary>
        public bool HasDictionaryDef { get; set; } = true;

        /// <summary>
        /// A CSV list of part of speech values to include (e.g., noun, verb).
        /// </summary>
        public List<PartOfSpeechType> IncludePartOfSpeech { get; set; } = new List<PartOfSpeechType>();

        /// <summary>
        /// A CSV list of part of speech values to exclude (e.g., noun, adjective).
        /// </summary>
        public List<PartOfSpeechType> ExcludePartOfSpeech { get; set; } = new List<PartOfSpeechType>();

        /// <summary>
        /// Minimum corpus frequency for terms.
        /// Default value is null (no minimum).
        /// </summary>
        public int? MinCorpusCount { get; set; }

        /// <summary>
        /// Maximum corpus frequency for terms.
        /// Default value is -1 (no maximum).
        /// </summary>
        public int? MaxCorpusCount { get; set; } = -1;

        /// <summary>
        /// Minimum dictionary count for returned words.
        /// Default value is 1.
        /// </summary>
        public int? MinDictionaryCount { get; set; } = 1;

        /// <summary>
        /// Maximum dictionary count for returned words.
        /// Default value is -1 (no maximum).
        /// </summary>
        public int? MaxDictionaryCount { get; set; } = -1;

        /// <summary>
        /// Minimum word length.
        /// Default value is 5.
        /// </summary>
        public int? MinLength { get; set; } = 5;

        /// <summary>
        /// Maximum word length.
        /// Default value is -1 (no maximum).
        /// </summary>
        public int? MaxLength { get; set; } = -1;

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

        /// <summary>
        /// Constructs the API query string from the request parameters.
        /// </summary>
        /// <returns>A query string to append to the API URL.</returns>
        public override string ToString()
        {
            var queryParameters = new List<string>
            {
                $"hasDictionaryDef={HasDictionaryDef.ToString().ToLower()}"
            };

            if (IncludePartOfSpeech != null && IncludePartOfSpeech.Any())
            {
                var partsOfSpeech = IncludePartOfSpeech.Select(pos => pos.ToApiString());
                queryParameters.Add($"includePartOfSpeech={string.Join(",", partsOfSpeech)}");
            }

            if (ExcludePartOfSpeech != null && ExcludePartOfSpeech.Any())
            {
                var partsOfSpeech = ExcludePartOfSpeech.Select(pos => pos.ToApiString());
                queryParameters.Add($"excludePartOfSpeech={string.Join(",", partsOfSpeech)}");
            }

            if (MinCorpusCount.HasValue)
            {
                queryParameters.Add($"minCorpusCount={MinCorpusCount.Value}");
            }

            if (MaxCorpusCount.HasValue)
            {
                queryParameters.Add($"maxCorpusCount={MaxCorpusCount.Value}");
            }

            if (MinDictionaryCount.HasValue)
            {
                queryParameters.Add($"minDictionaryCount={MinDictionaryCount.Value}");
            }

            if (MaxDictionaryCount.HasValue)
            {
                queryParameters.Add($"maxDictionaryCount={MaxDictionaryCount.Value}");
            }

            if (MinLength.HasValue) 
            {
                queryParameters.Add($"minLength={MinLength.Value}");
            }

            if (MaxLength.HasValue) 
            {
                queryParameters.Add($"maxLength={MaxLength.Value}");
            }

            if (SortBy != SortByType.NotSet)
            {
                queryParameters.Add($"sortBy={SortBy.ToApiString()}");
            }

            if (SortOrder != SortOrderType.NotSet)
            {
                queryParameters.Add($"sortOrder={SortOrder.ToApiString()}");
            }

            if (Limit.HasValue) {
                queryParameters.Add($"limit={Limit.Value}");
            }

            return string.Join("&", queryParameters);
        }
    }
}