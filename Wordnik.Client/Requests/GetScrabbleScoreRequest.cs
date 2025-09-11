using System;
using System.ComponentModel.DataAnnotations;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents a request to fetch the Scrabble score for a word.
    /// </summary>
    public class GetScrabbleScoreRequest : IWord
    {
        /// <summary>
        /// Gets or sets the word for which to fetch the Scrabble score.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Returns a string representation of the request for debugging or query construction.
        /// </summary>
        public override string ToString()
        {
            return $"word={Word}";
        }
    }
}
