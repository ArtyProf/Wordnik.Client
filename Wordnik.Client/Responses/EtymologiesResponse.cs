namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents etymologies under XML cover from the Wordnik API hyphenation response.
    /// </summary>
    public class EtymologiesResponse
    {
        /// <summary>
        /// Etymology description as an array of strings.
        /// Each string contains the historical linguistic explanation of a word.
        /// </summary>
        public string[] Etymologies { get; set; } = [];
    }
}
