namespace Hazel.Services.Media
{
    /// <summary>
    /// Represents default values related to media services.
    /// </summary>
    public static partial class HazelMediaDefaults
    {
        /// <summary>
        /// Gets the ThumbExistsCacheKey
        /// Gets a key to cache whether thumb exists.
        /// </summary>
        public static string ThumbExistsCacheKey => "Hazel.azure.thumb.exists-{0}";

        /// <summary>
        /// Gets the ThumbsPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string ThumbsPrefixCacheKey => "Hazel.azure.thumb";

        /// <summary>
        /// Gets the MultipleThumbDirectoriesLength
        /// Gets a multiple thumb directories length.
        /// </summary>
        public static int MultipleThumbDirectoriesLength => 3;

        /// <summary>
        /// Gets the ImageThumbsPath
        /// Gets a path to the image thumbs files.
        /// </summary>
        public static string ImageThumbsPath => @"images\thumbs";

        /// <summary>
        /// Gets the DefaultAvatarFileName
        /// Gets a default avatar file name.
        /// </summary>
        public static string DefaultAvatarFileName => "default-avatar.jpg";

        /// <summary>
        /// Gets the DefaultImageFileName
        /// Gets a default image file name.
        /// </summary>
        public static string DefaultImageFileName => "default-image.png";
    }
}
