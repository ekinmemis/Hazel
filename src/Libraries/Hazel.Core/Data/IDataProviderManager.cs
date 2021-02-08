namespace Hazel.Core.Data
{
    /// <summary>
    /// Represents a data provider manager.
    /// </summary>
    public partial interface IDataProviderManager
    {
        /// <summary>
        /// Gets the DataProvider
        /// Gets data provider.
        /// </summary>
        IDataProvider DataProvider { get; }
    }
}
