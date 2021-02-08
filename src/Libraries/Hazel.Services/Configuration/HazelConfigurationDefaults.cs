namespace Hazel.Services.Configuration
{
    /// <summary>
    /// Represents default values related to configuration services.
    /// </summary>
    public static partial class HazelConfigurationDefaults
    {
        /// <summary>
        /// Gets the SettingsAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string SettingsAllCacheKey => "Hazel.setting.all";

        /// <summary>
        /// Gets the SettingsPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string SettingsPrefixCacheKey => "Hazel.setting.";
    }
}
