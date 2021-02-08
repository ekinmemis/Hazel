namespace Hazel.Services.Seo
{
    /// <summary>
    /// Represents default values related to SEO services.
    /// </summary>
    public static partial class HazelSeoDefaults
    {
        /// <summary>
        /// Gets the ForumTopicLength
        /// Gets a max length of forum topic slug name.
        /// </summary>
        public static int ForumTopicLength => 100;

        /// <summary>
        /// Gets the SearchEngineNameLength
        /// Gets a max length of search engine name.
        /// </summary>
        public static int SearchEngineNameLength => 200;

        /// <summary>
        /// Gets the SitemapDateFormat
        /// Gets a date and time format for the sitemap.
        /// </summary>
        public static string SitemapDateFormat => @"yyyy-MM-dd";

        /// <summary>
        /// Gets the SitemapMaxUrlNumber
        /// Gets a max number of URLs in the sitemap file. At now each provided sitemap file must have no more than 50000 URLs.
        /// </summary>
        public static int SitemapMaxUrlNumber => 50000;

        /// <summary>
        /// Gets the UrlRecordActiveByIdNameLanguageCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string UrlRecordActiveByIdNameLanguageCacheKey => "Hazel.urlrecord.active.id-name-language-{0}-{1}-{2}";

        /// <summary>
        /// Gets the UrlRecordAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string UrlRecordAllCacheKey => "Hazel.urlrecord.all";

        /// <summary>
        /// Gets the UrlRecordBySlugCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string UrlRecordBySlugCacheKey => "Hazel.urlrecord.active.slug-{0}";

        /// <summary>
        /// Gets the UrlRecordPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string UrlRecordPrefixCacheKey => "Hazel.urlrecord.";
    }
}
