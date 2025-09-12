using System;

namespace Wordnik.Client.Responses
{
    /// <summary>
    /// Represents metadata for a specific audio file associated with a word.
    /// </summary>
    public class AudioResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for this audio metadata.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the word for which this audio file is relevant.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Gets or sets the type of the audio, such as 'pronunciation'.
        /// </summary>
        public string AudioType { get; set; }

        /// <summary>
        /// Gets or sets the public URL to access the audio file.
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the duration of the audio file in seconds.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Gets or sets the text attribution for the audio file.
        /// </summary>
        public string AttributionText { get; set; }

        /// <summary>
        /// Gets or sets the URL for the source of the attribution.
        /// </summary>
        public string AttributionUrl { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the audio was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the user or entity that created the audio file.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the description of the audio file.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of votes received for the audio file.
        /// </summary>
        public int VoteCount { get; set; }

        /// <summary>
        /// Gets or sets the average vote rating for the audio file.
        /// </summary>
        public double VoteAverage { get; set; }

        /// <summary>
        /// Gets or sets the weighted average vote rating for the audio file.
        /// </summary>
        public double VoteWeightedAverage { get; set; }

        /// <summary>
        /// Gets or sets the number of comments associated with the audio file.
        /// </summary>
        public int CommentCount { get; set; }
    }
}
