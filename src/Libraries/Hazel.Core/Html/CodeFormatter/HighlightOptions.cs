namespace Hazel.Core.Html.CodeFormatter
{
    /// <summary>
    /// Handles all of the options for changing the rendered code.
    /// </summary>
    public partial class HighlightOptions
    {
        /// <summary>
        /// Gets or sets the Code
        /// Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DisplayLineNumbers
        /// Display line numbers.
        /// </summary>
        public bool DisplayLineNumbers { get; set; }

        /// <summary>
        /// Gets or sets the Language
        /// Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AlternateLineNumbers
        /// Alternate line numbers.
        /// </summary>
        public bool AlternateLineNumbers { get; set; }
    }
}
