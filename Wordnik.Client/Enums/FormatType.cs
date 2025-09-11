using System.ComponentModel.DataAnnotations;

namespace Wordnik.Client.Enums
{
    /// <summary>
    /// Represents the type of text pronunciation format supported by the API.
    /// </summary>
    public enum FormatType
    {
        /// <summary>
        /// Represents all available formats.
        /// </summary>
        [Display(Name = "all")]
        All,

        /// <summary>
        /// AHD-5 pronunciation format.
        /// AHD stands for The American Heritage Dictionary, 5th Edition, providing phonetic pronunciations.
        /// </summary>
        [Display(Name = "ahd-5")]
        Ahd5,

        /// <summary>
        /// ARPAbet pronunciation format.
        /// ARPAbet provides phonemic notations and is often used in text-to-speech software.
        /// </summary>
        [Display(Name = "arpabet")]
        Arpabet,

        /// <summary>
        /// GCIDE Diacritical pronunciation format.
        /// GCIDE refers to the GNU Collaborative International Dictionary of English format using diacritical marks.
        /// </summary>
        [Display(Name = "gcide-diacritical")]
        GcideDiacritical,

        /// <summary>
        /// IPA pronunciation format.
        /// IPA stands for the International Phonetic Alphabet, which is a standardized system of phonetic notation.
        /// </summary>
        [Display(Name = "IPA")]
        Ipa
    }
}
