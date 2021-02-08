namespace Hazel.Core.Domain.Common
{
    /// <summary>
    /// Search term record (for statistics).
    /// </summary>
    public class SearchTermReportLine
    {
        /// <summary>
        /// Gets or sets the Keyword.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the Count
        /// Gets or sets search count.
        /// </summary>
        public int Count { get; set; }
    }
}
