using Hazel.Core.Configuration;

namespace Hazel.Core.Domain
{
    /// <summary>
    /// Store information settings.
    /// </summary>
    public class StoreInformationSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether "powered by hazelCommerce" text should be displayed.
        /// Please find more info at https://www.hazelcommerce.com/copyrightremoval.aspx.
        /// </summary>
        public bool HidePoweredByHazelCommerce { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether store is closed.
        /// </summary>
        public bool StoreClosed { get; set; }

        /// <summary>
        /// Gets or sets the LogoPictureId
        /// Gets or sets a picture identifier of the logo. If 0, then the default one will be used.
        /// </summary>
        public int LogoPictureId { get; set; }

        /// <summary>
        /// Gets or sets the DefaultStoreTheme
        /// Gets or sets a default store theme.
        /// </summary>
        public string DefaultStoreTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether applicationUsers are allowed to select a theme.
        /// </summary>
        public bool AllowApplicationUserToSelectTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mini profiler should be displayed in public store (used for debugging).
        /// </summary>
        public bool DisplayMiniProfilerInPublicStore { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mini profiler should be displayed only for users with access to the admin area.
        /// </summary>
        public bool DisplayMiniProfilerForAdminOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should display warnings about the new EU cookie law.
        /// </summary>
        public bool DisplayEuCookieLawWarning { get; set; }

        /// <summary>
        /// Gets or sets the FacebookLink
        /// Gets or sets a value of Facebook page URL of the site.
        /// </summary>
        public string FacebookLink { get; set; }

        /// <summary>
        /// Gets or sets the TwitterLink
        /// Gets or sets a value of Twitter page URL of the site.
        /// </summary>
        public string TwitterLink { get; set; }

        /// <summary>
        /// Gets or sets the YoutubeLink
        /// Gets or sets a value of YouTube channel URL of the site.
        /// </summary>
        public string YoutubeLink { get; set; }
    }
}
