using System;

namespace Wordnik.Client.Helpers
{
    /// <summary>
    /// A static class containing constants used throughout the Wordnik API client.
    /// </summary>
    public static class WordnikConstants
    {
        /// <summary>
        /// The base URL for the Wordnik API.
        /// </summary>
        public static string WordnikApiUrl => Environment.GetEnvironmentVariable("WordnikApiUrl") ?? "https://api.wordnik.com/v4/";

        /// <summary>
        /// The header name for the Wordnik API key.
        /// </summary>
        public const string WordnikApiKeyName = "api_key";

        /// <summary>
        /// The "audio" resource keyword used in the Word API.
        /// </summary>
        public const string Audio = "audio";

        /// <summary>
        /// The "definitions" resource keyword used in the Word API.
        /// </summary>
        public const string Definitions = "definitions";

        /// <summary>
        /// The "etymologies" resource keyword used in the Word API.
        /// </summary>
        public const string Etymologies = "etymologies";

        /// <summary>
        /// The "examples" resource keyword used in the Word API.
        /// </summary>
        public const string Examples = "examples";

        /// <summary>
        /// The "frequency" resource keyword used in the Word API.
        /// </summary>
        public const string Frequency = "frequency";

        /// <summary>
        /// The "hyphenation" resource keyword used in the Word API.
        /// </summary>
        public const string Hyphenation = "hyphenation";

        /// <summary>
        /// The "pronunciations" resource keyword used in the Word API.
        /// </summary>
        public const string Pronunciation = "pronunciations";

        /// <summary>
        /// The "relatedWords" resource keyword used in the Word API.
        /// </summary>
        public const string RelatedWords = "relatedWords";

        /// <summary>
        /// The "scrabbleScore" resource keyword used in the Word API.
        /// </summary>
        public const string ScrabbleScore = "scrabbleScore";

        /// <summary>
        /// The "topExample" resource keyword used in the Word API.
        /// </summary>
        public const string TopExample = "topExample";
    }
}
