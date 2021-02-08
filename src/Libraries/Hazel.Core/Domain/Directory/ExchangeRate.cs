using System;

namespace Hazel.Core.Domain.Directory
{
    /// <summary>
    /// Represents an exchange rate.
    /// </summary>
    public partial class ExchangeRate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeRate"/> class.
        /// </summary>
        public ExchangeRate()
        {
            CurrencyCode = string.Empty;
            Rate = 1.0m;
        }

        /// <summary>
        /// Gets or sets the CurrencyCode
        /// The three letter ISO code for the Exchange Rate, e.g. USD.
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the Rate
        /// The conversion rate of this currency from the base currency.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOn
        /// When was this exchange rate updated from the data source (the data XML feed).
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Format the rate into a string with the currency code, e.g. "USD 0.72543".
        /// </summary>
        /// <returns>.</returns>
        public override string ToString()
        {
            return $"{CurrencyCode} {Rate}";
        }
    }
}
