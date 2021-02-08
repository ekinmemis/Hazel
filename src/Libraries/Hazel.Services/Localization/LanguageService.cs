using Hazel.Core.Caching;
using Hazel.Core.Domain.Localization;
using Hazel.Data;
using Hazel.Services.Configuration;
using Hazel.Services.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hazel.Services.Localization
{
    /// <summary>
    /// Language service.
    /// </summary>
    public partial class LanguageService : ILanguageService
    {
        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _languageRepository.
        /// </summary>
        private readonly IRepository<Language> _languageRepository;

        /// <summary>
        /// Defines the _settingService.
        /// </summary>
        private readonly ISettingService _settingService;

        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly IStaticCacheManager _cacheManager;

        /// <summary>
        /// Defines the _localizationSettings.
        /// </summary>
        private readonly LocalizationSettings _localizationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="languageRepository">The languageRepository<see cref="IRepository{Language}"/>.</param>
        /// <param name="settingService">The settingService<see cref="ISettingService"/>.</param>
        /// <param name="cacheManager">The cacheManager<see cref="IStaticCacheManager"/>.</param>
        /// <param name="localizationSettings">The localizationSettings<see cref="LocalizationSettings"/>.</param>
        public LanguageService(IEventPublisher eventPublisher,
            IRepository<Language> languageRepository,
            ISettingService settingService,
            IStaticCacheManager cacheManager
            ,
            LocalizationSettings localizationSettings)
        {
            _eventPublisher = eventPublisher;
            _languageRepository = languageRepository;
            _settingService = settingService;
            _cacheManager = cacheManager;

            _localizationSettings = localizationSettings;
        }

        /// <summary>
        /// Deletes a language.
        /// </summary>
        /// <param name="language">Language.</param>
        public virtual void DeleteLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (language is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            //update default admin area language (if required)
            if (_localizationSettings.DefaultAdminLanguageId == language.Id)
            {
                foreach (var activeLanguage in GetAllLanguages())
                {
                    if (activeLanguage.Id == language.Id)
                        continue;

                    _localizationSettings.DefaultAdminLanguageId = activeLanguage.Id;
                    _settingService.SaveSetting(_localizationSettings);
                    break;
                }
            }

            _languageRepository.Delete(language);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LanguagesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(language);
        }

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records.</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching).</param>
        /// <returns>Languages.</returns>
        public virtual IList<Language> GetAllLanguages(bool showHidden = false, int storeId = 0, bool loadCacheableCopy = true)
        {
            IList<Language> LoadLanguagesFunc()
            {
                var query = _languageRepository.Table;
                if (!showHidden) query = query.Where(l => l.Published);
                query = query.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
                return query.ToList();
            }

            IList<Language> languages;
            if (loadCacheableCopy)
            {
                //cacheable copy
                var key = string.Format(HazelLocalizationDefaults.LanguagesAllCacheKey, showHidden);
                languages = _cacheManager.Get(key, () =>
                {
                    var result = new List<Language>();
                    foreach (var language in LoadLanguagesFunc())
                        result.Add(new LanguageForCaching(language));
                    return result;
                });
            }
            else
            {
                languages = LoadLanguagesFunc();
            }



            return languages;
        }

        /// <summary>
        /// Gets a language.
        /// </summary>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching).</param>
        /// <returns>Language.</returns>
        public virtual Language GetLanguageById(int languageId, bool loadCacheableCopy = true)
        {
            if (languageId == 0)
                return null;

            Language LoadLanguageFunc()
            {
                return _languageRepository.GetById(languageId);
            }

            if (!loadCacheableCopy)
                return LoadLanguageFunc();

            //cacheable copy
            var key = string.Format(HazelLocalizationDefaults.LanguagesByIdCacheKey, languageId);
            return _cacheManager.Get(key, () =>
            {
                var language = LoadLanguageFunc();
                return language == null ? null : new LanguageForCaching(language);
            });
        }

        /// <summary>
        /// Inserts a language.
        /// </summary>
        /// <param name="language">Language.</param>
        public virtual void InsertLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (language is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            _languageRepository.Insert(language);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LanguagesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(language);
        }

        /// <summary>
        /// Updates a language.
        /// </summary>
        /// <param name="language">Language.</param>
        public virtual void UpdateLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (language is IEntityForCaching)
                throw new ArgumentException("Cacheable entities are not supported by Entity Framework");

            //update language
            _languageRepository.Update(language);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LanguagesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(language);
        }

        /// <summary>
        /// Get 2 letter ISO language code.
        /// </summary>
        /// <param name="language">Language.</param>
        /// <returns>ISO language code.</returns>
        public virtual string GetTwoLetterIsoLanguageName(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (string.IsNullOrEmpty(language.LanguageCulture))
                return "en";

            var culture = new CultureInfo(language.LanguageCulture);
            var code = culture.TwoLetterISOLanguageName;

            return string.IsNullOrEmpty(code) ? "en" : code;
        }
    }
}
