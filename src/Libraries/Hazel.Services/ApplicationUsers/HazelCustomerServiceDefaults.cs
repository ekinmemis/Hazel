namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// Represents default values related to applicationUser services.
    /// </summary>
    public static partial class HazelApplicationUserServiceDefaults
    {
        /// <summary>
        /// Gets the ApplicationUserAttributesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserAttributesAllCacheKey => "hazel.applicationUserattribute.all";

        /// <summary>
        /// Gets the ApplicationUserAttributesByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserAttributesByIdCacheKey => "hazel.applicationUserattribute.id-{0}";

        /// <summary>
        /// Gets the ApplicationUserAttributeValuesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserAttributeValuesAllCacheKey => "hazel.applicationUserattributevalue.all-{0}";

        /// <summary>
        /// Gets the ApplicationUserAttributeValuesByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserAttributeValuesByIdCacheKey => "hazel.applicationUserattributevalue.id-{0}";

        /// <summary>
        /// Gets the ApplicationUserAttributesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string ApplicationUserAttributesPrefixCacheKey => "hazel.applicationUserattribute.";

        /// <summary>
        /// Gets the ApplicationUserAttributeValuesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string ApplicationUserAttributeValuesPrefixCacheKey => "hazel.applicationUserattributevalue.";

        /// <summary>
        /// Gets the ApplicationUserRolesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserRolesAllCacheKey => "hazel.applicationUserrole.all-{0}";

        /// <summary>
        /// Gets the ApplicationUserRolesBySystemNameCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string ApplicationUserRolesBySystemNameCacheKey => "hazel.applicationUserrole.systemname-{0}";

        /// <summary>
        /// Gets the ApplicationUserRolesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string ApplicationUserRolesPrefixCacheKey => "hazel.applicationUserrole.";

        /// <summary>
        /// Gets the ApplicationUserPasswordLifetimeCacheKey
        /// Gets a key for caching current applicationUser password lifetime.
        /// </summary>
        public static string ApplicationUserPasswordLifetimeCacheKey => "hazel.applicationUsers.passwordlifetime-{0}";

        /// <summary>
        /// Gets the PasswordSaltKeySize
        /// Gets a password salt key size.
        /// </summary>
        public static int PasswordSaltKeySize => 5;

        /// <summary>
        /// Gets the ApplicationUserUsernameLength
        /// Gets a max username length.
        /// </summary>
        public static int ApplicationUserUsernameLength => 100;

        /// <summary>
        /// Gets the DefaultHashedPasswordFormat
        /// Gets a default hash format for applicationUser password.
        /// </summary>
        public static string DefaultHashedPasswordFormat => "SHA512";
    }
}
