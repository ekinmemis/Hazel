namespace Hazel.Services.Directory
{
    /// <summary>
    /// Represents default values related to directory services.
    /// </summary>
    public static partial class HazelDirectoryDefaults
    {
        /// <summary>
        /// Gets the CountriesByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CountriesByIdCacheKey => "Hazel.country.id-{0}";

        /// <summary>
        /// Gets the CountriesByTwoLetterCodeCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CountriesByTwoLetterCodeCacheKey => "Hazel.country.twoletter-{0}";

        /// <summary>
        /// Gets the CountriesByThreeLetterCodeCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CountriesByThreeLetterCodeCacheKey => "Hazel.country.threeletter-{0}";

        /// <summary>
        /// Gets the CountriesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CountriesAllCacheKey => "Hazel.country.all-{0}-{1}";

        /// <summary>
        /// Gets the CountriesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string CountriesPrefixCacheKey => "Hazel.country.";

        /// <summary>
        /// Gets the CurrenciesByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CurrenciesByIdCacheKey => "Hazel.currency.id-{0}";

        /// <summary>
        /// Gets the CurrenciesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string CurrenciesAllCacheKey => "Hazel.currency.all-{0}";

        /// <summary>
        /// Gets the CurrenciesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string CurrenciesPrefixCacheKey => "Hazel.currency.";

        /// <summary>
        /// Gets the MeasureDimensionsAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string MeasureDimensionsAllCacheKey => "Hazel.measuredimension.all";

        /// <summary>
        /// Gets the MeasureDimensionsByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string MeasureDimensionsByIdCacheKey => "Hazel.measuredimension.id-{0}";

        /// <summary>
        /// Gets the MeasureWeightsAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string MeasureWeightsAllCacheKey => "Hazel.measureweight.all";

        /// <summary>
        /// Gets the MeasureWeightsByIdCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string MeasureWeightsByIdCacheKey => "Hazel.measureweight.id-{0}";

        /// <summary>
        /// Gets the MeasureDimensionsPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string MeasureDimensionsPrefixCacheKey => "Hazel.measuredimension.";

        /// <summary>
        /// Gets the MeasureWeightsPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string MeasureWeightsPrefixCacheKey => "Hazel.measureweight.";

        /// <summary>
        /// Gets the StateProvincesAllCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string StateProvincesAllCacheKey => "Hazel.stateprovince.all-{0}-{1}-{2}";

        /// <summary>
        /// Gets the StateProvincesByAbbreviationCacheKey
        /// Gets a key for caching.
        /// </summary>
        public static string StateProvincesByAbbreviationCacheKey => "Hazel.stateprovince.abbreviationcountryid-{0}";

        /// <summary>
        /// Gets the StateProvincesPrefixCacheKey
        /// Gets a key pattern to clear cache.
        /// </summary>
        public static string StateProvincesPrefixCacheKey => "Hazel.stateprovince.";
    }
}
