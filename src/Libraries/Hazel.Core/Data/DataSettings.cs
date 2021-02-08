using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Hazel.Core.Data
{
    /// <summary>
    /// Represents the data settings.
    /// </summary>
    public partial class DataSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSettings"/> class.
        /// </summary>
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the DataProvider
        /// Gets or sets a data provider.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DataProviderType DataProvider { get; set; }

        /// <summary>
        /// Gets or sets the DataConnectionString
        /// Gets or sets a connection string.
        /// </summary>
        public string DataConnectionString { get; set; }

        /// <summary>
        /// Gets the RawDataSettings
        /// Gets or sets a raw settings.
        /// </summary>
        public IDictionary<string, string> RawDataSettings { get; }

        /// <summary>
        /// Gets a value indicating whether IsValid
        /// Gets or sets a value indicating whether the information is entered.
        /// </summary>
        [JsonIgnore]
        public bool IsValid => DataProvider != DataProviderType.Unknown && !string.IsNullOrEmpty(DataConnectionString);
    }
}
