using System.Data.Common;

namespace Hazel.Core.Data
{
    /// <summary>
    /// Represents a data provider.
    /// </summary>
    public partial interface IDataProvider
    {
        /// <summary>
        /// Initialize database.
        /// </summary>
        void InitializeDatabase();

        /// <summary>
        /// Get a support database parameter object (used by stored procedures).
        /// </summary>
        /// <returns>Parameter.</returns>
        DbParameter GetParameter();

        /// <summary>
        /// Gets the BackupSupported
        /// Gets a value indicating whether this data provider supports backup.
        /// </summary>
        bool BackupSupported { get; }

        /// <summary>
        /// Gets the SupportedLengthOfBinaryHash
        /// Gets a maximum length of the data for HASHBYTES functions, returns 0 if HASHBYTES function is not supported.
        /// </summary>
        int SupportedLengthOfBinaryHash { get; }
    }
}
