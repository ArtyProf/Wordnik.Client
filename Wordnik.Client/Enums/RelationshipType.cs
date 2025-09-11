using System.ComponentModel.DataAnnotations;

namespace Wordnik.Client.Enums
{
    /// <summary>
    /// Represents the types of word relationships available in the Wordnik API.
    /// </summary>
    public enum RelationshipType
    {
        /// <summary>
        /// Represents all available types of word relationships.
        /// </summary>
        [Display(Name = "all")]
        All,
        /// <summary>
        /// Represents a synonym relationship.
        /// A synonym is a word or phrase that means exactly or nearly the same as another word.
        /// </summary>
        [Display(Name = "synonym")]
        Synonym,

        /// <summary>
        /// Represents an antonym relationship.
        /// An antonym is a word with a meaning opposite to that of another word.
        /// </summary>
        [Display(Name = "antonym")]
        Antonym,

        /// <summary>
        /// Represents a variant relationship.
        /// A variant is an alternative form of the same word.
        /// </summary>
        [Display(Name = "variant")]
        Variant,

        /// <summary>
        /// Represents an equivalent relationship.
        /// An equivalent refers to a word that corresponds in meaning or effect.
        /// </summary>
        [Display(Name = "equivalent")]
        Equivalent,

        /// <summary>
        /// Represents a cross-reference relationship.
        /// A cross-reference links related words or entries in a dictionary.
        /// </summary>
        [Display(Name = "cross-reference")]
        CrossReference,

        /// <summary>
        /// Represents a related-word relationship.
        /// Related words are associated by general meaning or usage.
        /// </summary>
        [Display(Name = "related-word")]
        RelatedWord,

        /// <summary>
        /// Represents a rhyme relationship.
        /// Words that rhyme have the same ending sound.
        /// </summary>
        [Display(Name = "rhyme")]
        Rhyme,

        /// <summary>
        /// Represents a form relationship.
        /// A form shows inflections or derived versions of a word.
        /// </summary>
        [Display(Name = "form")]
        Form,

        /// <summary>
        /// Represents an etymologically-related term.
        /// Refers to terms that share a common historical origin.
        /// </summary>
        [Display(Name = "etymologically-related-term")]
        EtymologicallyRelatedTerm,

        /// <summary>
        /// Represents a hypernym relationship.
        /// A hypernym is a broader term for a more specific word (e.g., 'color' is a hypernym of 'red').
        /// </summary>
        [Display(Name = "hypernym")]
        Hypernym,

        /// <summary>
        /// Represents a hyponym relationship.
        /// A hyponym is a more specific term within a broader category (e.g., 'red' is a hyponym of 'color').
        /// </summary>
        [Display(Name = "hyponym")]
        Hyponym,

        /// <summary>
        /// Represents an inflected form of a word.
        /// Inflected forms show changes in grammar or meaning (e.g., plural or tense changes).
        /// </summary>
        [Display(Name = "inflected-form")]
        InflectedForm,

        /// <summary>
        /// Represents the primary usage or meaning of a word.
        /// </summary>
        [Display(Name = "primary")]
        Primary,

        /// <summary>
        /// Represents a same-context relationship.
        /// Words used in the same context or setting.
        /// </summary>
        [Display(Name = "same-context")]
        SameContext,

        /// <summary>
        /// Represents a verb-form relationship.
        /// Verb forms include conjugations or derivatives of verbs.
        /// </summary>
        [Display(Name = "verb-form")]
        VerbForm,

        /// <summary>
        /// Represents a verb-stem relationship.
        /// A verb stem refers to the base or root form of a verb.
        /// </summary>
        [Display(Name = "verb-stem")]
        VerbStem,

        /// <summary>
        /// Represents a "has_topic" relationship for words grouped by a shared topic or subject.
        /// </summary>
        [Display(Name = "has_topic")]
        HasTopic
    }
}