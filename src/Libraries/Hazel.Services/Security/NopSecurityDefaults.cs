namespace Hazel.Services.Security
{
    /// <summary>
    /// Represents default values related to security services.
    /// </summary>
    public static partial class HazelSecurityDefaults
    {
        /// <summary>
        /// Gets the AclRecordByEntityIdNameCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string AclRecordByEntityIdNameCacheKey => "Hazel.aclrecord.entityid-name-{0}-{1}";

        /// <summary>
        /// Gets the AclRecordPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string AclRecordPrefixCacheKey => "Hazel.aclrecord.";

        /// <summary>
        /// Gets the PermissionsAllowedCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string PermissionsAllowedCacheKey => "Hazel.permission.allowed-{0}-{1}";

        /// <summary>
        /// Gets the PermissionsAllByApplicationUserRoleIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string PermissionsAllByApplicationUserRoleIdCacheKey => "Hazel.permission.allbyApplicationUserroleid-{0}";

        /// <summary>
        /// Gets the PermissionsPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string PermissionsPrefixCacheKey => "Hazel.permission.";

        /// <summary>
        /// Gets the RecaptchaApiUrl
        /// Gets a base reCAPTCHA API URL.
        /// </summary>
        public static string RecaptchaApiUrl => "https://www.google.com/recaptcha/";

        /// <summary>
        /// Gets the RecaptchaScriptPath
        /// Gets a reCAPTCHA script URL.
        /// </summary>
        public static string RecaptchaScriptPath => "api.js?onload=onloadCallback{0}&render=explicit{1}";

        /// <summary>
        /// Gets the RecaptchaValidationPath
        /// Gets a reCAPTCHA validation URL.
        /// </summary>
        public static string RecaptchaValidationPath => "api/siteverify?secret={0}&response={1}&remoteip={2}";
    }
}
