namespace Hazel.Core.ComponentModel
{
    /// <summary>
    /// Reader/Write locker type
    /// </summary>
    public enum ReaderWriteLockType
    {
        /// <summary>
        /// Defines the Read.
        /// </summary>
        Read,

        /// <summary>
        /// Defines the Write.
        /// </summary>
        Write,

        /// <summary>
        /// Defines the UpgradeableRead.
        /// </summary>
        UpgradeableRead
    }
}
