using Hazel.Core.Caching;
using Hazel.Core.Domain.Directory;
using Hazel.Data;
using Hazel.Services.Events;
using Hazel.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Directory
{
    /// <summary>
    /// Country service.
    /// </summary>
    public partial class CountryService : ICountryService
    {
        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _localizationService.
        /// </summary>
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Defines the _countryRepository.
        /// </summary>
        private readonly IRepository<Country> _countryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService"/> class.
        /// </summary>
        /// <param name="cacheManager">The cacheManager<see cref="ICacheManager"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="localizationService">The localizationService<see cref="ILocalizationService"/>.</param>
        /// <param name="countryRepository">The countryRepository<see cref="IRepository{Country}"/>.</param>
        public CountryService(
            ICacheManager cacheManager,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IRepository<Country> countryRepository)
        {
            ;
            _cacheManager = cacheManager;
            _eventPublisher = eventPublisher;
            _localizationService = localizationService;
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// Deletes a country.
        /// </summary>
        /// <param name="country">Country.</param>
        public virtual void DeleteCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            _countryRepository.Delete(country);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CountriesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(country);
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it.</param>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>Countries.</returns>
        public virtual IList<Country> GetAllCountries(int languageId = 0, bool showHidden = false)
        {
            var key = string.Format(HazelDirectoryDefaults.CountriesAllCacheKey, languageId, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = _countryRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name);

                var countries = query.ToList();

                if (languageId > 0)
                {
                    //we should sort countries by localized names when they have the same display order
                    countries = countries
                        .OrderBy(c => c.DisplayOrder)
                        .ThenBy(c => _localizationService.GetLocalized(c, x => x.Name, languageId))
                        .ToList();
                }

                return countries;
            });
        }

        /// <summary>
        /// Gets all countries that allow billing.
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it.</param>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>Countries.</returns>
        public virtual IList<Country> GetAllCountriesForBilling(int languageId = 0, bool showHidden = false)
        {
            return GetAllCountries(languageId, showHidden).Where(c => c.AllowsBilling).ToList();
        }

        /// <summary>
        /// Gets all countries that allow shipping.
        /// </summary>
        /// <param name="languageId">Language identifier. It's used to sort countries by localized names (if specified); pass 0 to skip it.</param>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>Countries.</returns>
        public virtual IList<Country> GetAllCountriesForShipping(int languageId = 0, bool showHidden = false)
        {
            return GetAllCountries(languageId, showHidden).Where(c => c.AllowsShipping).ToList();
        }

        /// <summary>
        /// Gets a country.
        /// </summary>
        /// <param name="countryId">Country identifier.</param>
        /// <returns>Country.</returns>
        public virtual Country GetCountryById(int countryId)
        {
            if (countryId == 0)
                return null;

            var key = string.Format(HazelDirectoryDefaults.CountriesByIdCacheKey, countryId);
            return _cacheManager.Get(key, () => _countryRepository.GetById(countryId));
        }

        /// <summary>
        /// Get countries by identifiers.
        /// </summary>
        /// <param name="countryIds">Country identifiers.</param>
        /// <returns>Countries.</returns>
        public virtual IList<Country> GetCountriesByIds(int[] countryIds)
        {
            if (countryIds == null || countryIds.Length == 0)
                return new List<Country>();

            var query = from c in _countryRepository.Table
                        where countryIds.Contains(c.Id)
                        select c;
            var countries = query.ToList();
            //sort by passed identifiers
            var sortedCountries = new List<Country>();
            foreach (var id in countryIds)
            {
                var country = countries.Find(x => x.Id == id);
                if (country != null)
                    sortedCountries.Add(country);
            }

            return sortedCountries;
        }

        /// <summary>
        /// Gets a country by two letter ISO code.
        /// </summary>
        /// <param name="twoLetterIsoCode">Country two letter ISO code.</param>
        /// <returns>Country.</returns>
        public virtual Country GetCountryByTwoLetterIsoCode(string twoLetterIsoCode)
        {
            if (string.IsNullOrEmpty(twoLetterIsoCode))
                return null;

            var key = string.Format(HazelDirectoryDefaults.CountriesByTwoLetterCodeCacheKey, twoLetterIsoCode);
            return _cacheManager.Get(key, () =>
            {
                var query = from c in _countryRepository.Table
                            where c.TwoLetterIsoCode == twoLetterIsoCode
                            select c;
                return query.FirstOrDefault();
            });
        }

        /// <summary>
        /// Gets a country by three letter ISO code.
        /// </summary>
        /// <param name="threeLetterIsoCode">Country three letter ISO code.</param>
        /// <returns>Country.</returns>
        public virtual Country GetCountryByThreeLetterIsoCode(string threeLetterIsoCode)
        {
            if (string.IsNullOrEmpty(threeLetterIsoCode))
                return null;

            var key = string.Format(HazelDirectoryDefaults.CountriesByThreeLetterCodeCacheKey, threeLetterIsoCode);
            return _cacheManager.Get(key, () =>
            {
                var query = from c in _countryRepository.Table
                            where c.ThreeLetterIsoCode == threeLetterIsoCode
                            select c;
                return query.FirstOrDefault();
            });
        }

        /// <summary>
        /// Inserts a country.
        /// </summary>
        /// <param name="country">Country.</param>
        public virtual void InsertCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            _countryRepository.Insert(country);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CountriesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(country);
        }

        /// <summary>
        /// Updates the country.
        /// </summary>
        /// <param name="country">Country.</param>
        public virtual void UpdateCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            _countryRepository.Update(country);

            _cacheManager.RemoveByPrefix(HazelDirectoryDefaults.CountriesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(country);
        }
    }
}
