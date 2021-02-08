using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Directory
{
    /// <summary>
    /// Currency settings.
    /// </summary>
    public class CurrencySettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether DisplayCurrencyLabel
        /// A value indicating whether to display currency labels.
        /// </summary>
        public bool DisplayCurrencyLabel { get; set; }

        /// <summary>
        /// Gets or sets the PrimaryStoreCurrencyId
        /// Primary store currency identifier.
        /// </summary>
        public int PrimaryStoreCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the PrimaryExchangeRateCurrencyId
        /// Primary exchange rate currency identifier.
        /// </summary>
        public int PrimaryExchangeRateCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the ActiveExchangeRateProviderSystemName
        /// Active exchange rate provider system name (of a plugin).
        /// </summary>
        public string ActiveExchangeRateProviderSystemName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AutoUpdateEnabled
        /// A value indicating whether to enable automatic currency rate updates.
        /// </summary>
        public bool AutoUpdateEnabled { get; set; }
    }
}
