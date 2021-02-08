using System;

namespace Hazel.Core.Domain.Media
{
    /// <summary>
    /// Helper class for making pictures hashes from DB.
    /// </summary>
    public partial class PictureHashItem : IComparable, IComparable<PictureHashItem>
    {
        /// <summary>
        /// Gets or sets the PictureId.
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the Hash.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// The CompareTo.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as PictureHashItem);
        }

        /// <summary>
        /// The CompareTo.
        /// </summary>
        /// <param name="other">The other<see cref="PictureHashItem"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(PictureHashItem other)
        {
            return other == null ? -1 : PictureId.CompareTo(other.PictureId);
        }
    }
}
