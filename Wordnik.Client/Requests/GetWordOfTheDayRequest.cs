using System;

namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Represents the request parameters for retrieving Word of the Day data from the Wordnik API.
    /// </summary>
    public class GetWordOfTheDayRequest
    {
        /// <summary>
        /// The date for which to fetch the Word of the Day.
        /// </summary>
        /// <remarks>
        /// The date is optional. If not set, the API may default to the Word of the Day for the current date.
        /// </remarks>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Constructs the query string representation of all parameters for this request.
        /// </summary>
        /// <returns>A query string compatible with the Wordnik API.</returns>
        public override string ToString()
        {
            if (Date.HasValue)
            {
                return $"date={Date.Value.ToString("yyyy-MM-dd")}";
            }

            return "";
        }
    }
}