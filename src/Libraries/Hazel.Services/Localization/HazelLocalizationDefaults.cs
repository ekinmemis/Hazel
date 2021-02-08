namespace Hazel.Services.Localization
{
    /// <summary>
    /// Represents default values related to localization services.
    /// </summary>
    public static partial class HazelLocalizationDefaults
    {
        /// <summary>
        /// Gets the LanguagesByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LanguagesByIdCacheKey => "Hazel.language.id-{0}";

        /// <summary>
        /// Gets the LanguagesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LanguagesAllCacheKey => "Hazel.language.all-{0}";

        /// <summary>
        /// Gets the LanguagesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string LanguagesPrefixCacheKey => "Hazel.language.";

        /// <summary>
        /// Gets the LocaleStringResourcesAllPublicCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocaleStringResourcesAllPublicCacheKey => "Hazel.lsr.all.public-{0}";

        /// <summary>
        /// Gets the LocaleStringResourcesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocaleStringResourcesAllCacheKey => "Hazel.lsr.all-{0}";

        /// <summary>
        /// Gets the LocaleStringResourcesAllAdminCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocaleStringResourcesAllAdminCacheKey => "Hazel.lsr.all.admin-{0}";

        /// <summary>
        /// Gets the LocaleStringResourcesByResourceNameCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocaleStringResourcesByResourceNameCacheKey => "Hazel.lsr.{0}-{1}";

        /// <summary>
        /// Gets the LocaleStringResourcesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string LocaleStringResourcesPrefixCacheKey => "Hazel.lsr.";

        /// <summary>
        /// Gets the AdminLocaleStringResourcesPrefix
        /// Gets a prefix of locale resources for the admin area.
        /// </summary>
        public static string AdminLocaleStringResourcesPrefix => "Admin.";

        /// <summary>
        /// Gets the EnumLocaleStringResourcesPrefix
        /// Gets a prefix of locale resources for enumerations.
        /// </summary>
        public static string EnumLocaleStringResourcesPrefix => "Enums.";

        /// <summary>
        /// Gets the PermissionLocaleStringResourcesPrefix
        /// Gets a prefix of locale resources for permissions.
        /// </summary>
        public static string PermissionLocaleStringResourcesPrefix => "Permission.";

        /// <summary>
        /// Gets the PluginNameLocaleStringResourcesPrefix
        /// Gets a prefix of locale resources for plugin friendly names.
        /// </summary>
        public static string PluginNameLocaleStringResourcesPrefix => "Plugins.FriendlyName.";

        /// <summary>
        /// Gets the LocalizedPropertyCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocalizedPropertyCacheKey => "Hazel.localizedproperty.value-{0}-{1}-{2}-{3}";

        /// <summary>
        /// Gets the LocalizedPropertyAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string LocalizedPropertyAllCacheKey => "Hazel.localizedproperty.all";

        /// <summary>
        /// Gets the LocalizedPropertyPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string LocalizedPropertyPrefixCacheKey => "Hazel.localizedproperty.";
    }
}
