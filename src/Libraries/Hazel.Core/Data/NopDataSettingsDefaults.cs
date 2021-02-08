namespace Hazel.Core.Data
{
    /// <summary>
    /// Represents default values related to data settings.
    /// </summary>
    public static partial class HazelDataSettingsDefaults
    {
        /// <summary>
        /// Gets the ObsoleteFilePath
        /// Gets a path to the file that was used in old hazel versions to contain data settings.
        /// </summary>
        public static string ObsoleteFilePath => "~/App_Data/Settings.txt";

        /// <summary>
        /// Gets the FilePath
        /// Gets a path to the file that contains data settings.
        /// </summary>
        public static string FilePath => "~/App_Data/dataSettings.json";
    }
}
