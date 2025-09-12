namespace Wordnik.Client.Requests
{
    /// <summary>
    /// Request model for retrieving a random word from the Wordnik API.
    /// </summary>
    public class GetRandomWordRequest : RandomWordBaseRequest
    {
        /// <inheritdoc />
        public override string ToString()
        {
            // Use only base query string for single random word request
            return GenerateBaseQueryString();
        }
    }
}