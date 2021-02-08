using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Localization
{
    /// <summary>
    /// Localization settings.
    /// </summary>
    public class LocalizationSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the DefaultAdminLanguageId
        /// Default admin area language identifier.
        /// </summary>
        public int DefaultAdminLanguageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseImagesForLanguageSelection
        /// Use images for language selection.
        /// </summary>
        public bool UseImagesForLanguageSelection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SeoFriendlyUrlsForLanguagesEnabled
        /// A value indicating whether SEO friendly URLs with multiple languages are enabled.
        /// </summary>
        public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AutomaticallyDetectLanguage
        /// A value indicating whether we should detect the current language by a applicationUser region (browser settings).
        /// </summary>
        public bool AutomaticallyDetectLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether LoadAllLocaleRecordsOnStartup
        /// A value indicating whether to load all LocaleStringResource records on application startup.
        /// </summary>
        public bool LoadAllLocaleRecordsOnStartup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether LoadAllLocalizedPropertiesOnStartup
        /// A value indicating whether to load all LocalizedProperty records on application startup.
        /// </summary>
        public bool LoadAllLocalizedPropertiesOnStartup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether LoadAllUrlRecordsOnStartup
        /// A value indicating whether to load all search engine friendly names (slugs) on application startup.
        /// </summary>
        public bool LoadAllUrlRecordsOnStartup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IgnoreRtlPropertyForAdminArea
        /// A value indicating whether to we should ignore RTL language property for admin area.
        /// It's useful for store owners with RTL languages for public store but preferring LTR for admin area.
        /// </summary>
        public bool IgnoreRtlPropertyForAdminArea { get; set; }
    }
}
