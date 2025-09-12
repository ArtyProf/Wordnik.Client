using System.ComponentModel.DataAnnotations;

namespace Wordnik.Client.Enums
{
    /// <summary>
    /// Specifies the sort attribute for the random words request.
    /// </summary>
    public enum SortByType
    {
        /// <summary>
        /// Sort not set.
        /// </summary>
        NotSet,

        /// <summary>
        /// Sort alphabetically.
        /// </summary>
        [Display(Name = "alpha")]
        Alphabetical,

        /// <summary>
        /// Sort by corpus frequency count.
        /// </summary>
        [Display(Name = "count")]
        Count
    }
}