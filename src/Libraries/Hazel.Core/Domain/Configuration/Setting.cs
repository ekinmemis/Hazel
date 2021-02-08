using Hazel.Core.Domain.Localization;

namespace Hazel.Core.Domain.Configuration
{
    /// <summary>
    /// Represents a setting.
    /// </summary>
    public partial class Setting : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        public Setting()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        /// <param name="storeId">Store identifier.</param>
        public Setting(string name, string value, int storeId = 0)
        {
            Name = name;
            Value = value;
            StoreId = storeId;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the store for which this setting is valid. 0 is set when the setting is for all stores.
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>Result.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
