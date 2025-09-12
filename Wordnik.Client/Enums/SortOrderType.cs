using System.ComponentModel.DataAnnotations;

namespace Wordnik.Client.Enums
{
    /// <summary>
    /// Specifies the sort direction for the random words request.
    /// </summary>
    public enum SortOrderType
    {
        /// <summary>
        /// Sort in not set.
        /// </summary>
        NotSet,

        /// <summary>
        /// Sort in ascending order.
        /// </summary>
        [Display(Name = "asc")]
        Ascending,

        /// <summary>
        /// Sort in descending order.
        /// </summary>
        [Display(Name = "desc")]
        Descending
    }
}