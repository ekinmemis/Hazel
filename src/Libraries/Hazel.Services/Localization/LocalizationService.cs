using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Configuration;
using Hazel.Core.Data;
using Hazel.Core.Domain.Localization;
using Hazel.Core.Domain.Security;
using Hazel.Data;
using Hazel.Data.Extensions;
using Hazel.Services.Configuration;
using Hazel.Services.Events;
using Hazel.Services.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Hazel.Services.Localization
{
    /// <summary>
    /// Provides information about localization.
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        /// <summary>
        /// Defines the _dataProvider.
        /// </summary>
        private readonly IDataProvider _dataProvider;

        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _languageService.
        /// </summary>
        private readonly ILanguageService _languageService;

        /// <summary>
        /// Defines the _localizedEntityService.
        /// </summary>
        private readonly ILocalizedEntityService _localizedEntityService;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Defines the _lsrRepository.
        /// </summary>
        private readonly IRepository<LocaleStringResource> _lsrRepository;

        /// <summary>
        /// Defines the _settingService.
        /// </summary>
        private readonly ISettingService _settingService;

        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly IStaticCacheManager _cacheManager;

        /// <summary>
        /// Defines the _workContext.
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// Defines the _localizationSettings.
        /// </summary>
        private readonly LocalizationSettings _localizationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationService"/> class.
        /// </summary>
        /// <param name="dataProvider">The dataProvider<see cref="IDataProvider"/>.</param>
        /// <param name="dbContext">The dbContext<see cref="IDbContext"/>.</param>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="languageService">The languageService<see cref="ILanguageService"/>.</param>
        /// <param name="localizedEntityService">The localizedEntityService<see cref="ILocalizedEntityService"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="lsrRepository">The lsrRepository<see cref="IRepository{LocaleStringResource}"/>.</param>
        /// <param name="settingService">The settingService<see cref="ISettingService"/>.</param>
        /// <param name="cacheManager">The cacheManager<see cref="IStaticCacheManager"/>.</param>
        /// <param name="workContext">The workContext<see cref="IWorkContext"/>.</param>
        /// <param name="localizationSettings">The localizationSettings<see cref="LocalizationSettings"/>.</param>
        public LocalizationService(IDataProvider dataProvider,
            IDbContext dbContext,
            IEventPublisher eventPublisher,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            ILogger logger,
            IRepository<LocaleStringResource> lsrRepository,
            ISettingService settingService,
            IStaticCacheManager cacheManager,
            IWorkContext workContext,
            LocalizationSettings localizationSettings)
        {
            _dataProvider = dataProvider;
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
            _languageService = languageService;
            _localizedEntityService = localizedEntityService;
            _logger = logger;
            _lsrRepository = lsrRepository;
            _settingService = settingService;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
        }

        /// <summary>
        /// Insert resources.
        /// </summary>
        /// <param name="resources">Resources.</param>
        protected virtual void InsertLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //insert
            _lsrRepository.Insert(resources);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);

            //event notification
            foreach (var resource in resources)
            {
                _eventPublisher.EntityInserted(resource);
            }
        }

        /// <summary>
        /// Update resources.
        /// </summary>
        /// <param name="resources">Resources.</param>
        protected virtual void UpdateLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //update
            _lsrRepository.Update(resources);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);

            //event notification
            foreach (var resource in resources)
            {
                _eventPublisher.EntityUpdated(resource);
            }
        }

        /// <summary>
        /// The ResourceValuesToDictionary.
        /// </summary>
        /// <param name="locales">The locales<see cref="IEnumerable{LocaleStringResource}"/>.</param>
        /// <returns>The <see cref="Dictionary{string, KeyValuePair{int, string}}"/>.</returns>
        private static Dictionary<string, KeyValuePair<int, string>> ResourceValuesToDictionary(IEnumerable<LocaleStringResource> locales)
        {
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }

            return dictionary;
        }

        /// <summary>
        /// Deletes a locale string resource.
        /// </summary>
        /// <param name="localeStringResource">Locale string resource.</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Delete(localeStringResource);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(localeStringResource);
        }

        /// <summary>
        /// Gets a locale string resource.
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier.</param>
        /// <returns>Locale string resource.</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;

            return _lsrRepository.GetById(localeStringResourceId);
        }

        /// <summary>
        /// Gets a locale string resource.
        /// </summary>
        /// <param name="resourceName">A string representing a resource name.</param>
        /// <returns>Locale string resource.</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            if (_workContext.WorkingLanguage != null)
                return GetLocaleStringResourceByName(resourceName, _workContext.WorkingLanguage.Id);

            return null;
        }

        /// <summary>
        /// Gets a locale string resource.
        /// </summary>
        /// <param name="resourceName">A string representing a resource name.</param>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found.</param>
        /// <returns>Locale string resource.</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId,
            bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId && lsr.ResourceName == resourceName
                        select lsr;
            var localeStringResource = query.FirstOrDefault();

            if (localeStringResource == null && logIfNotFound)
                _logger.Warning($"Resource string ({resourceName}) not found. Language ID = {languageId}");
            return localeStringResource;
        }

        /// <summary>
        /// Gets all locale string resources by language identifier.
        /// </summary>
        /// <param name="languageId">Language identifier.</param>
        /// <returns>Locale string resources.</returns>
        public virtual IList<LocaleStringResource> GetAllResources(int languageId)
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;
            var locales = query.ToList();
            return locales;
        }

        /// <summary>
        /// Inserts a locale string resource.
        /// </summary>
        /// <param name="localeStringResource">Locale string resource.</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Insert(localeStringResource);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(localeStringResource);
        }

        /// <summary>
        /// Updates the locale string resource.
        /// </summary>
        /// <param name="localeStringResource">Locale string resource.</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Update(localeStringResource);

            //cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(localeStringResource);
        }

        /// <summary>
        /// Gets all locale string resources by language identifier.
        /// </summary>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="loadPublicLocales">A value indicating whether to load data for the public store only (if "false", then for admin area only. If null, then load all locales. We use it for performance optimization of the site startup.</param>
        /// <returns>Locale string resources.</returns>
        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId, bool? loadPublicLocales)
        {
            var key = string.Format(HazelLocalizationDefaults.LocaleStringResourcesAllCacheKey, languageId);

            //get all locale string resources by language identifier
            if (!loadPublicLocales.HasValue || _cacheManager.IsSet(key))
            {
                var rez = _cacheManager.Get(key, () =>
                {
                    //we use no tracking here for performance optimization
                    //anyway records are loaded only for read-only operations
                    var query = from l in _lsrRepository.TableNoTracking
                                orderby l.ResourceName
                                where l.LanguageId == languageId
                                select l;

                    return ResourceValuesToDictionary(query);
                });

                //remove separated resource 
                _cacheManager.Remove(string.Format(HazelLocalizationDefaults.LocaleStringResourcesAllPublicCacheKey, languageId));
                _cacheManager.Remove(string.Format(HazelLocalizationDefaults.LocaleStringResourcesAllAdminCacheKey, languageId));

                return rez;
            }

            //performance optimization of the site startup
            key = string.Format(loadPublicLocales.Value ? HazelLocalizationDefaults.LocaleStringResourcesAllPublicCacheKey : HazelLocalizationDefaults.LocaleStringResourcesAllAdminCacheKey, languageId);

            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in _lsrRepository.TableNoTracking
                            orderby l.ResourceName
                            where l.LanguageId == languageId
                            select l;
                query = loadPublicLocales.Value ? query.Where(r => !r.ResourceName.StartsWith(HazelLocalizationDefaults.AdminLocaleStringResourcesPrefix)) : query.Where(r => r.ResourceName.StartsWith(HazelLocalizationDefaults.AdminLocaleStringResourcesPrefix));
                return ResourceValuesToDictionary(query);
            });
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {
            if (_workContext.WorkingLanguage != null)
                return GetResource(resourceKey, _workContext.WorkingLanguage.Id);

            return string.Empty;
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var resources = GetAllResourceValues(languageId, !resourceKey.StartsWith(HazelLocalizationDefaults.AdminLocaleStringResourcesPrefix, StringComparison.InvariantCultureIgnoreCase));
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
            }
            else
            {
                //gradual loading
                var key = string.Format(HazelLocalizationDefaults.LocaleStringResourcesByResourceNameCacheKey, languageId, resourceKey);
                var lsr = _cacheManager.Get(key, () =>
                {
                    var query = from l in _lsrRepository.Table
                                where l.ResourceName == resourceKey
                                && l.LanguageId == languageId
                                select l.ResourceValue;
                    return query.FirstOrDefault();
                });

                if (lsr != null)
                    result = lsr;
            }

            if (!string.IsNullOrEmpty(result))
                return result;

            if (logIfNotFound)
                _logger.Warning($"Resource string ({resourceKey}) is not found. Language ID = {languageId}");

            if (!string.IsNullOrEmpty(defaultValue))
            {
                result = defaultValue;
            }
            else
            {
                if (!returnEmptyIfNotFound)
                    result = resourceKey;
            }

            return result;
        }

        /// <summary>
        /// Export language resources to XML.
        /// </summary>
        /// <param name="language">Language.</param>
        /// <returns>Result in XML format.</returns>
        public virtual string ExportResourcesToXml(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));
            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Language");
                    xmlWriter.WriteAttributeString("Name", language.Name);
                    xmlWriter.WriteAttributeString("SupportedVersion", HazelVersion.CurrentVersion);

                    var resources = GetAllResources(language.Id);
                    foreach (var resource in resources)
                    {
                        xmlWriter.WriteStartElement("LocaleResource");
                        xmlWriter.WriteAttributeString("Name", resource.ResourceName);
                        xmlWriter.WriteElementString("Value", null, resource.ResourceValue);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Import language resources from XML file.
        /// </summary>
        /// <param name="language">Language.</param>
        /// <param name="xmlStreamReader">Stream reader of XML file.</param>
        /// <param name="updateExistingResources">A value indicating whether to update existing resources.</param>
        public virtual void ImportResourcesFromXml(Language language, StreamReader xmlStreamReader, bool updateExistingResources = true)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (xmlStreamReader.EndOfStream)
                return;

            //stored procedures are enabled and supported by the database.
            var pLanguageId = _dataProvider.GetParameter();
            pLanguageId.ParameterName = "LanguageId";
            pLanguageId.Value = language.Id;
            pLanguageId.DbType = DbType.Int32;

            var pXmlPackage = _dataProvider.GetParameter();
            pXmlPackage.ParameterName = "XmlPackage";
            pXmlPackage.Value = new SqlXml(XmlReader.Create(xmlStreamReader));
            pXmlPackage.DbType = DbType.Xml;

            var pUpdateExistingResources = _dataProvider.GetParameter();
            pUpdateExistingResources.ParameterName = "UpdateExistingResources";
            pUpdateExistingResources.Value = updateExistingResources;
            pUpdateExistingResources.DbType = DbType.Boolean;

            //long-running query. specify timeout (600 seconds)
            _dbContext.ExecuteSqlCommand("EXEC [LanguagePackImport] @LanguageId, @XmlPackage, @UpdateExistingResources",
                false, 600, pLanguageId, pXmlPackage, pUpdateExistingResources);

            //clear cache
            _cacheManager.RemoveByPrefix(HazelLocalizationDefaults.LocaleStringResourcesPrefixCacheKey);
        }

        /// <summary>
        /// Get localized property of an entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="languageId">Language identifier; pass null to use the current working language; pass 0 to get standard language value.</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found).</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value.</param>
        /// <returns>Localized property.</returns>
        public virtual TPropType GetLocalized<TEntity, TPropType>(TEntity entity, Expression<Func<TEntity, TPropType>> keySelector,
            int? languageId = null, bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where TEntity : BaseEntity, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!(keySelector.Body is MemberExpression member))
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

            if (!(member.Member is PropertyInfo propInfo))
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

            var result = default(TPropType);
            var resultStr = string.Empty;

            var localeKeyGroup = entity.GetUnproxiedEntityType().Name;
            var localeKey = propInfo.Name;

            if (!languageId.HasValue)
                languageId = _workContext.WorkingLanguage.Id;

            if (languageId > 0)
            {
                //ensure that we have at least two published languages
                var loadLocalizedValue = true;
                if (ensureTwoPublishedLanguages)
                {
                    var totalPublishedLanguages = _languageService.GetAllLanguages().Count;
                    loadLocalizedValue = totalPublishedLanguages >= 2;
                }

                //localized value
                if (loadLocalizedValue)
                {
                    resultStr = _localizedEntityService
                        .GetLocalizedValue(languageId.Value, entity.Id, localeKeyGroup, localeKey);
                    if (!string.IsNullOrEmpty(resultStr))
                        result = CommonHelper.To<TPropType>(resultStr);
                }
            }

            //set default value if required
            if (!string.IsNullOrEmpty(resultStr) || !returnDefaultValue)
                return result;
            var localizer = keySelector.Compile();
            result = localizer(entity);

            return result;
        }

        /// <summary>
        /// Get localized property of setting.
        /// </summary>
        /// <typeparam name="TSettings">Settings type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found).</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value.</param>
        /// <returns>Localized property.</returns>
        public virtual string GetLocalizedSetting<TSettings>(TSettings settings, Expression<Func<TSettings, string>> keySelector,
            int languageId, int storeId, bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where TSettings : ISettings, new()
        {
            var key = _settingService.GetSettingKey(settings, keySelector);

            //we do not support localized settings per store (overridden store settings)
            var setting = _settingService.GetSetting(key, storeId: storeId, loadSharedValueIfNotFound: true);
            if (setting == null)
                return null;

            return GetLocalized(setting, x => x.Value, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }

        /// <summary>
        /// Save localized property of setting.
        /// </summary>
        /// <typeparam name="TSettings">Settings type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="languageId">Language identifier.</param>
        /// <param name="value">Localized value.</param>
        public virtual void SaveLocalizedSetting<TSettings>(TSettings settings, Expression<Func<TSettings, string>> keySelector,
            int languageId, string value) where TSettings : ISettings, new()
        {
            var key = _settingService.GetSettingKey(settings, keySelector);

            //we do not support localized settings per store (overridden store settings)
            var setting = _settingService.GetSetting(key);
            if (setting == null)
                return;

            _localizedEntityService.SaveLocalizedValue(setting, x => x.Value, value, languageId);
        }

        /// <summary>
        /// Get localized value of enum.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="enumValue">Enum value.</param>
        /// <param name="languageId">Language identifier; pass null to use the current working language.</param>
        /// <returns>Localized value.</returns>
        public virtual string GetLocalizedEnum<TEnum>(TEnum enumValue, int? languageId = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            //localized value
            var resourceName = $"{HazelLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(TEnum)}.{enumValue}";
            var result = GetResource(resourceName, languageId ?? _workContext.WorkingLanguage.Id, false, string.Empty, true);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }

        /// <summary>
        /// Get localized value of enum
        /// We don't have UI to manage permission localizable name. That's why we're using this method.
        /// </summary>
        /// <param name="permissionRecord">Permission record.</param>
        /// <param name="languageId">Language identifier; pass null to use the current working language.</param>
        /// <returns>Localized value.</returns>
        public virtual string GetLocalizedPermissionName(PermissionRecord permissionRecord, int? languageId = null)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));

            //localized value
            var resourceName = $"{HazelLocalizationDefaults.PermissionLocaleStringResourcesPrefix}{permissionRecord.SystemName}";
            var result = GetResource(resourceName, languageId ?? _workContext.WorkingLanguage.Id, false, string.Empty, true);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = permissionRecord.Name;

            return result;
        }

        /// <summary>
        /// Save localized name of a permission.
        /// </summary>
        /// <param name="permissionRecord">Permission record.</param>
        public virtual void SaveLocalizedPermissionName(PermissionRecord permissionRecord)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));

            var resourceName = $"{HazelLocalizationDefaults.PermissionLocaleStringResourcesPrefix}{permissionRecord.SystemName}";
            var resourceValue = permissionRecord.Name;

            foreach (var lang in _languageService.GetAllLanguages(true))
            {
                var lsr = GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr == null)
                {
                    lsr = new LocaleStringResource
                    {
                        LanguageId = lang.Id,
                        ResourceName = resourceName,
                        ResourceValue = resourceValue
                    };
                    InsertLocaleStringResource(lsr);
                }
                else
                {
                    lsr.ResourceValue = resourceValue;
                    UpdateLocaleStringResource(lsr);
                }
            }
        }

        /// <summary>
        /// Delete a localized name of a permission.
        /// </summary>
        /// <param name="permissionRecord">Permission record.</param>
        public virtual void DeleteLocalizedPermissionName(PermissionRecord permissionRecord)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));

            var resourceName = $"{HazelLocalizationDefaults.PermissionLocaleStringResourcesPrefix}{permissionRecord.SystemName}";
            foreach (var lang in _languageService.GetAllLanguages(true))
            {
                var lsr = GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr != null)
                    DeleteLocaleStringResource(lsr);
            }
        }

        /// <summary>
        /// Add a locale resource (if new) or update an existing one.
        /// </summary>
        /// <param name="resourceName">Resource name.</param>
        /// <param name="resourceValue">Resource value.</param>
        /// <param name="languageCulture">Language culture code. If null or empty, then a resource will be added for all languages.</param>
        public virtual void AddOrUpdatePluginLocaleResource(string resourceName, string resourceValue, string languageCulture = null)
        {
            foreach (var lang in _languageService.GetAllLanguages(true))
            {
                if (!string.IsNullOrEmpty(languageCulture) && !languageCulture.Equals(lang.LanguageCulture))
                    continue;

                var lsr = GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr == null)
                {
                    lsr = new LocaleStringResource
                    {
                        LanguageId = lang.Id,
                        ResourceName = resourceName,
                        ResourceValue = resourceValue
                    };
                    InsertLocaleStringResource(lsr);
                }
                else
                {
                    lsr.ResourceValue = resourceValue;
                    UpdateLocaleStringResource(lsr);
                }
            }
        }

        /// <summary>
        /// Delete a locale resource.
        /// </summary>
        /// <param name="resourceName">Resource name.</param>
        public virtual void DeletePluginLocaleResource(string resourceName)
        {
            foreach (var lang in _languageService.GetAllLanguages(true))
            {
                var lsr = GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr != null)
                    DeleteLocaleStringResource(lsr);
            }
        }
    }
}
