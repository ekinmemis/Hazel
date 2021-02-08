namespace Hazel.Core.Http
{
    /// <summary>
    /// Represents default values related to cookies.
    /// </summary>
    public static partial class HazelCookieDefaults
    {
        /// <summary>
        /// Gets the cookie name prefix.
        /// </summary>
        public static string Prefix => ".Hazel";

        /// <summary>
        /// Gets the ApplicationUserCookie
        /// Gets a cookie name of the applicationUser.
        /// </summary>
        public static string ApplicationUserCookie => ".ApplicationUser";

        /// <summary>
        /// Gets the AntiforgeryCookie
        /// Gets a cookie name of the antiforgery.
        /// </summary>
        public static string AntiforgeryCookie => ".Antiforgery";

        /// <summary>
        /// Gets the SessionCookie
        /// Gets a cookie name of the session state.
        /// </summary>
        public static string SessionCookie => ".Session";

        /// <summary>
        /// Gets the TempDataCookie
        /// Gets a cookie name of the temp data.
        /// </summary>
        public static string TempDataCookie => ".TempData";

        /// <summary>
        /// Gets the InstallationLanguageCookie
        /// Gets a cookie name of the installation language.
        /// </summary>
        public static string InstallationLanguageCookie => ".InstallationLanguage";

        /// <summary>
        /// Gets the ComparedProductsCookie
        /// Gets a cookie name of the compared products.
        /// </summary>
        public static string ComparedProductsCookie => ".ComparedProducts";

        /// <summary>
        /// Gets the RecentlyViewedProductsCookie
        /// Gets a cookie name of the recently viewed products.
        /// </summary>
        public static string RecentlyViewedProductsCookie => ".RecentlyViewedProducts";

        /// <summary>
        /// Gets the AuthenticationCookie
        /// Gets a cookie name of the authentication.
        /// </summary>
        public static string AuthenticationCookie => ".Authentication";

        /// <summary>
        /// Gets the ExternalAuthenticationCookie
        /// Gets a cookie name of the external authentication.
        /// </summary>
        public static string ExternalAuthenticationCookie => ".ExternalAuthentication";

        /// <summary>
        /// Gets the IgnoreEuCookieLawWarning
        /// Gets a cookie name of the Eu Cookie Law Warning.
        /// </summary>
        public static string IgnoreEuCookieLawWarning => ".IgnoreEuCookieLawWarning";
    }
}
