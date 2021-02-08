using System.Collections.Generic;

namespace Hazel.Core
{
    /// <summary>
    /// Paged list interface.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IPagedList<TEntity> : IList<TEntity>
    {
        /// <summary>
        /// Gets the PageIndex
        /// Page index.
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Gets the PageSize
        /// Page size.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the TotalCount
        /// Total count.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the TotalPages
        /// Total pages.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets the HasPreviousPage
        /// Has previous page.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets the HasNextPage
        /// Has next age.
        /// </summary>
        bool HasNextPage { get; }
    }
}
