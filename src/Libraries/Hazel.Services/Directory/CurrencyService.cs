using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Domain.Directory;
using Hazel.Data;
using Hazel.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Directory
{
    /// <summary>
    /// Currency service.
    /// </summary>
    public partial class CurrencyService : ICurrencyService
    {
        /// <summary>
        /// Defines the _currencySettings.
        /// </summary>
        private readonly CurrencySettings _currencySettings;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _currencyRepository.
        /// </summary>
        private readonly IRepository<Currency> _currencyRepository;

        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly IStaticCacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="currencySettings">The currencySettings<see cref="CurrencySettings"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="currencyRepository">The currencyRepository<see cref="IRepository{Currency}"/>.</param>
        /// <param name="cacheManager">The cacheManager<see cref="IStaticCacheManager"/>.</param>
        public CurrencyService(CurrencySettings currencySettings,
            IEventPublisher eventPublisher,
            IRepository<Currency> currencyRepository,
            IStaticCacheManager cacheManager)
        {
            _currencySettings = currencySettings;
            _eventPublisher = eventPublisher;
            _currencyRepository = currencyRepository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Deletes currency.
        /// </summary>
        /// <param name="currency">Currency.</param>
        public virtual void DeleteCurrency(Currency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            if (currency is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            _currencyRepository.Delete(currency);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CurrenciesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(currency);
        }

        /// <summary>
        /// Gets a currency.
        /// </summary>
        /// <param name="currencyId">Currency identifier.</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching).</param>
        /// <returns>Currency.</returns>
        public virtual Currency GetCurrencyById(int currencyId, bool loadCacheableCopy = true)
        {
            if (currencyId == 0)
                return null;

            Currency loadCurrencyFunc()
            {
                return _currencyRepository.GetById(currencyId);
            }

            if (!loadCacheableCopy)
                return loadCurrencyFunc();

            //cacheable copy
            var key = string.Format(HazelDirectoryDefaults.CurrenciesByIdCacheKey, currencyId);
            return _cacheManager.Get(key, () =>
            {
                var currency = loadCurrencyFunc();
                if (currency == null)
                    return null;
                return new CurrencyForCaching(currency);
            });
        }

        /// <summary>
        /// Gets a currency by code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching).</param>
        /// <returns>Currency.</returns>
        public virtual Currency GetCurrencyByCode(string currencyCode, bool loadCacheableCopy = true)
        {
            if (string.IsNullOrEmpty(currencyCode))
                return null;
            return GetAllCurrencies(true, loadCacheableCopy: loadCacheableCopy)
                .FirstOrDefault(c => c.CurrencyCode.ToLower() == currencyCode.ToLower());
        }

        /// <summary>
        /// Gets all currencies.
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records.</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching).</param>
        /// <returns>Currencies.</returns>
        public virtual IList<Currency> GetAllCurrencies(bool showHidden = false, int storeId = 0, bool loadCacheableCopy = true)
        {
            IList<Currency> loadCurrenciesFunc()
            {
                var query = _currencyRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Id);
                return query.ToList();
            }

            IList<Currency> currencies;
            if (loadCacheableCopy)
            {
                //cacheable copy
                var key = string.Format(HazelDirectoryDefaults.CurrenciesAllCacheKey, showHidden);
                currencies = _cacheManager.Get(key, () =>
                {
                    var result = new List<Currency>();
                    foreach (var currency in loadCurrenciesFunc())
                        result.Add(new CurrencyForCaching(currency));
                    return result;
                });
            }
            else
            {
                currencies = loadCurrenciesFunc();
            }

            return currencies;
        }

        /// <summary>
        /// Inserts a currency.
        /// </summary>
        /// <param name="currency">Currency.</param>
        public virtual void InsertCurrency(Currency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            if (currency is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            _currencyRepository.Insert(currency);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CurrenciesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(currency);
        }

        /// <summary>
        /// Updates the currency.
        /// </summary>
        /// <param name="currency">Currency.</param>
        public virtual void UpdateCurrency(Currency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            if (currency is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            _currencyRepository.Update(currency);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CurrenciesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(currency);
        }

        /// <summary>
        /// Converts currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="exchangeRate">Currency exchange rate.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertCurrency(decimal amount, decimal exchangeRate)
        {
            if (amount != decimal.Zero && exchangeRate != decimal.Zero)
                return amount * exchangeRate;
            return decimal.Zero;
        }

        /// <summary>
        /// Converts currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="sourceCurrencyCode">Source currency code.</param>
        /// <param name="targetCurrencyCode">Target currency code.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertCurrency(decimal amount, Currency sourceCurrencyCode, Currency targetCurrencyCode)
        {
            if (sourceCurrencyCode == null)
                throw new ArgumentNullException(nameof(sourceCurrencyCode));

            if (targetCurrencyCode == null)
                throw new ArgumentNullException(nameof(targetCurrencyCode));

            var result = amount;
            if (sourceCurrencyCode.Id == targetCurrencyCode.Id)
                return result;

            if (result == decimal.Zero || sourceCurrencyCode.Id == targetCurrencyCode.Id)
                return result;

            result = ConvertToPrimaryExchangeRateCurrency(result, sourceCurrencyCode);
            result = ConvertFromPrimaryExchangeRateCurrency(result, targetCurrencyCode);
            return result;
        }

        /// <summary>
        /// Converts to primary exchange rate currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="sourceCurrencyCode">Source currency code.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertToPrimaryExchangeRateCurrency(decimal amount, Currency sourceCurrencyCode)
        {
            if (sourceCurrencyCode == null)
                throw new ArgumentNullException(nameof(sourceCurrencyCode));

            var primaryExchangeRateCurrency = GetCurrencyById(_currencySettings.PrimaryExchangeRateCurrencyId);
            if (primaryExchangeRateCurrency == null)
                throw new Exception("Primary exchange rate currency cannot be loaded");

            var result = amount;
            if (result == decimal.Zero || sourceCurrencyCode.Id == primaryExchangeRateCurrency.Id)
                return result;

            var exchangeRate = sourceCurrencyCode.Rate;
            if (exchangeRate == decimal.Zero)
                throw new HazelException($"Exchange rate not found for currency [{sourceCurrencyCode.Name}]");
            result = result / exchangeRate;

            return result;
        }

        /// <summary>
        /// Converts from primary exchange rate currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="targetCurrencyCode">Target currency code.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertFromPrimaryExchangeRateCurrency(decimal amount, Currency targetCurrencyCode)
        {
            if (targetCurrencyCode == null)
                throw new ArgumentNullException(nameof(targetCurrencyCode));

            var primaryExchangeRateCurrency = GetCurrencyById(_currencySettings.PrimaryExchangeRateCurrencyId);
            if (primaryExchangeRateCurrency == null)
                throw new Exception("Primary exchange rate currency cannot be loaded");

            var result = amount;
            if (result == decimal.Zero || targetCurrencyCode.Id == primaryExchangeRateCurrency.Id)
                return result;

            var exchangeRate = targetCurrencyCode.Rate;
            if (exchangeRate == decimal.Zero)
                throw new HazelException($"Exchange rate not found for currency [{targetCurrencyCode.Name}]");
            result = result * exchangeRate;

            return result;
        }

        /// <summary>
        /// Converts to primary store currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="sourceCurrencyCode">Source currency code.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertToPrimaryStoreCurrency(decimal amount, Currency sourceCurrencyCode)
        {
            if (sourceCurrencyCode == null)
                throw new ArgumentNullException(nameof(sourceCurrencyCode));

            var primaryStoreCurrency = GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            var result = ConvertCurrency(amount, sourceCurrencyCode, primaryStoreCurrency);
            return result;
        }

        /// <summary>
        /// Converts from primary store currency.
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <param name="targetCurrencyCode">Target currency code.</param>
        /// <returns>Converted value.</returns>
        public virtual decimal ConvertFromPrimaryStoreCurrency(decimal amount, Currency targetCurrencyCode)
        {
            var primaryStoreCurrency = GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            var result = ConvertCurrency(amount, primaryStoreCurrency, targetCurrencyCode);
            return result;
        }
    }
}
