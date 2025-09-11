using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents the response for a word relationship query.
    /// </summary>
    public class RelatedWordsResponse
    {
        /// <summary>
        /// The type of relationship for the word.
        /// </summary>
        [JsonProperty("relationshipType")]
        public string RelationshipType { get; set; }

        /// <summary>
        /// The list of words associated with the relationship type.
        /// </summary>
        [JsonProperty("words")]
        public List<string> Words { get; set; }
    }
}
