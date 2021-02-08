namespace Hazel.Services.Common
{
    /// <summary>
    /// Represents default values related to common services.
    /// </summary>
    public static partial class HazelCommonDefaults
    {
        /// <summary>
        /// Gets the KeepAlivePath
        /// Gets a request path to the keep alive URL.
        /// </summary>
        public static string KeepAlivePath => "keepalive/index";

        /// <summary>
        /// Gets the GenericAttributeCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string GenericAttributeCacheKey => "Hazel.genericattribute.{0}-{1}";

        /// <summary>
        /// Gets the GenericAttributePrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string GenericAttributePrefixCacheKey => "Hazel.genericattribute.";

        /// <summary>
        /// Gets the HeadCodeFileName
        /// Gets a name of the file with code for the head element.
        /// </summary>
        public static string HeadCodeFileName => "html_code.html";

        /// <summary>
        /// Gets the FaviconAndAppIconsPath
        /// Gets a path to the favicon and app icons.
        /// </summary>
        public static string FaviconAndAppIconsPath => "icons\\icons_{0}";

        /// <summary>
        /// Gets the OldFaviconIconName
        /// Gets a name of the old favicon icon for current store.
        /// </summary>
        public static string OldFaviconIconName => "favicon-{0}.ico";

        /// <summary>
        /// Gets the HazelCopyrightWarningPath
        /// Gets a path to request the hazel official site for copyright warning.
        /// </summary>
        public static string HazelCopyrightWarningPath => "SiteWarnings.aspx?local={0}&url={1}";

        /// <summary>
        /// Gets the HazelNewsRssPath
        /// Gets a path to request the hazel official site for news RSS.
        /// </summary>
        public static string HazelNewsRssPath => "NewsRSS.aspx?Version={0}&Localhost={1}&HideAdvertisements={2}&StoreURL={3}";

        /// <summary>
        /// Gets the HazelExtensionsCategoriesPath
        /// Gets a path to request the hazel official site for available categories of marketplace extensions.
        /// </summary>
        public static string HazelExtensionsCategoriesPath => "ExtensionsXml.aspx?getCategories=1";

        /// <summary>
        /// Gets the HazelExtensionsVersionsPath
        /// Gets a path to request the hazel official site for available versions of marketplace extensions.
        /// </summary>
        public static string HazelExtensionsVersionsPath => "ExtensionsXml.aspx?getVersions=1";

        /// <summary>
        /// Gets the HazelExtensionsPath
        /// Gets a path to request the hazel official site for marketplace extensions.
        /// </summary>
        public static string HazelExtensionsPath => "ExtensionsXml.aspx?category={0}&version={1}&price={2}&searchTerm={3}&pageIndex={4}&pageSize={5}";
    }
}
