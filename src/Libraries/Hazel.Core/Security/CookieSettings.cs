using Hazel.Core.Configuration;

namespace Hazel.Core.Security
{
    /// <summary>
    /// Defines the <see cref="CookieSettings" />.
    /// </summary>
    public partial class CookieSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the CompareProductsCookieExpires
        /// Expiration time on hours for the "Compare products" cookie.
        /// </summary>
        public int CompareProductsCookieExpires { get; set; }

        /// <summary>
        /// Gets or sets the RecentlyViewedProductsCookieExpires
        /// Expiration time on hours for the "Recently viewed products" cookie.
        /// </summary>
        public int RecentlyViewedProductsCookieExpires { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationUserCookieExpires
        /// Expiration time on hours for the "ApplicationUser" cookie.
        /// </summary>
        public int ApplicationUserCookieExpires { get; set; }
    }
}
