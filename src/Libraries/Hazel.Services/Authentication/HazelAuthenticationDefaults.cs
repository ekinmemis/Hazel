using Microsoft.AspNetCore.Http;

namespace Hazel.Services.Authentication
{
    /// <summary>
    /// Represents default values related to authentication services.
    /// </summary>
    public static partial class HazelAuthenticationDefaults
    {
        /// <summary>
        /// Gets the AuthenticationScheme
        /// The default value used for authentication scheme.
        /// </summary>
        public static string AuthenticationScheme => "Authentication";

        /// <summary>
        /// Gets the ExternalAuthenticationScheme
        /// The default value used for external authentication scheme.
        /// </summary>
        public static string ExternalAuthenticationScheme => "ExternalAuthentication";

        /// <summary>
        /// Gets the ClaimsIssuer
        /// The issuer that should be used for any claims that are created.
        /// </summary>
        public static string ClaimsIssuer => "hazel";

        /// <summary>
        /// Gets the LoginPath
        /// The default value for the login path.
        /// </summary>
        public static PathString LoginPath => new PathString("/login");

        /// <summary>
        /// Gets the LogoutPath
        /// The default value used for the logout path.
        /// </summary>
        public static PathString LogoutPath => new PathString("/logout");

        /// <summary>
        /// Gets the AccessDeniedPath
        /// The default value for the access denied path.
        /// </summary>
        public static PathString AccessDeniedPath => new PathString("/page-not-found");

        /// <summary>
        /// Gets the ReturnUrlParameter
        /// The default value of the return URL parameter.
        /// </summary>
        public static string ReturnUrlParameter => string.Empty;

        /// <summary>
        /// Gets the ExternalAuthenticationErrorsSessionKey
        /// Gets a key to store external authentication errors to session.
        /// </summary>
        public static string ExternalAuthenticationErrorsSessionKey => "nop.externalauth.errors";
    }
}
