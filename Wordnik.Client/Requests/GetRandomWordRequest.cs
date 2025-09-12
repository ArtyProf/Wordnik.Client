using System.Collections.Generic;
using System.Linq;
using Wordnik.Client.Enums;
using Wordnik.Client.Helpers;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Request model for retrieving a random word from the Wordnik API.
    /// </summary>
    public class GetRandomWordRequest
    {
        /// <summary>
        /// Whether to only return words with dictionary definitions.
        /// Default value is true.
        /// </summary>
        public bool HasDictionaryDef { get; set; } = true;

        /// <summary>
        /// A CSV list of part of speech values to include. 
        /// Examples: noun, verb, adjective, etc.
        /// </summary>
        public List<PartOfSpeechType> IncludePartOfSpeech { get; set; } = new List<PartOfSpeechType>();

        /// <summary>
        /// A CSV list of part of speech values to exclude.
        /// Examples: noun, verb, adjective, etc.
        /// </summary>
        public List<PartOfSpeechType> ExcludePartOfSpeech { get; set; } = new List<PartOfSpeechType>();

        /// <summary>
        /// The minimum corpus frequency for returned words.
        /// Default value is null, meaning no minimum filter is applied.
        /// </summary>
        public int? MinCorpusCount { get; set; }

        /// <summary>
        /// The maximum corpus frequency for returned words.
        /// Default value is -1, meaning no maximum filter is applied.
        /// </summary>
        public int? MaxCorpusCount { get; set; } = -1;

        /// <summary>
        /// The minimum dictionary count for returned words.
        /// Default value is 1.
        /// </summary>
        public int? MinDictionaryCount { get; set; } = 1;

        /// <summary>
        /// The maximum dictionary count for returned words.
        /// Default value is -1, meaning no maximum filter is applied.
        /// </summary>
        public int? MaxDictionaryCount { get; set; } = -1;

        /// <summary>
        /// The minimum length of the word.
        /// Default value is 5.
        /// </summary>
        public int? MinLength { get; set; } = 5;

        /// <summary>
        /// The maximum length of the word.
        /// Default value is -1, meaning no maximum filter is applied.
        /// </summary>
        public int? MaxLength { get; set; } = -1;

        /// <summary>
        /// Constructs the query string representation of all included parameters for this request.
        /// </summary>
        /// <returns>A query string that can be appended to the API URL.</returns>
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

            if (MinCorpusCount.HasValue) queryParameters.Add($"minCorpusCount={MinCorpusCount.Value}");
            if (MaxCorpusCount.HasValue) queryParameters.Add($"maxCorpusCount={MaxCorpusCount.Value}");
            if (MinDictionaryCount.HasValue) queryParameters.Add($"minDictionaryCount={MinDictionaryCount.Value}");
            if (MaxDictionaryCount.HasValue) queryParameters.Add($"maxDictionaryCount={MaxDictionaryCount.Value}");
            if (MinLength.HasValue) queryParameters.Add($"minLength={MinLength.Value}");
            if (MaxLength.HasValue) queryParameters.Add($"maxLength={MaxLength.Value}");

            return string.Join("&", queryParameters);
        }
    }
}