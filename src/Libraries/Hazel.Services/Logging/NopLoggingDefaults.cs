namespace Hazel.Services.Logging
{
    /// <summary>
    /// Represents default values related to logging services.
    /// </summary>
    public static partial class HazelLoggingDefaults
    {
        /// <summary>
        /// Gets the ActivityTypeAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ActivityTypeAllCacheKey => "Hazel.activitytype.all";

        /// <summary>
        /// Gets the ActivityTypePrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string ActivityTypePrefixCacheKey => "Hazel.activitytype.";
    }
}
