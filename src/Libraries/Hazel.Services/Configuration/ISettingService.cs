using Hazel.Core.Configuration;
using Hazel.Core.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hazel.Services.Configuration
{
    /// <summary>
    /// Setting service interface.
    /// </summary>
    public partial interface ISettingService
    {
        /// <summary>
        /// Gets a setting by identifier.
        /// </summary>
        /// <param name="settingId">Setting identifier.</param>
        /// <returns>Setting.</returns>
        Setting GetSettingById(int settingId);

        /// <summary>
        /// Deletes a setting.
        /// </summary>
        /// <param name="setting">Setting.</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// Deletes settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        void DeleteSettings(IList<Setting> settings);

        /// <summary>
        /// Get setting by key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found.</param>
        /// <returns>Setting.</returns>
        Setting GetSetting(string key, int storeId = 0, bool loadSharedValueIfNotFound = false);

        /// <summary>
        /// Get setting value by key.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared (for all stores) value should be loaded if a value specific for a certain is not found.</param>
        /// <returns>Setting value.</returns>
        TEntity GetSettingByKey<TEntity>(string key, TEntity defaultValue = default(TEntity),
            int storeId = 0, bool loadSharedValueIfNotFound = false);

        /// <summary>
        /// Set setting value.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update.</param>
        void SetSetting<TEntity>(string key, TEntity value, int storeId = 0, bool clearCache = true);

        /// <summary>
        /// Gets all settings.
        /// </summary>
        /// <returns>Settings.</returns>
        IList<Setting> GetAllSettings();

        /// <summary>
        /// Determines whether a setting exists.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <returns>true -setting exists; false - does not exist.</returns>
        bool SettingExists<TEntity, TPropType>(TEntity settings,
            Expression<Func<TEntity, TPropType>> keySelector, int storeId = 0)
            where TEntity : ISettings, new();

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="storeId">Store identifier for which settings should be loaded.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        TEntity LoadSetting<TEntity>(int storeId = 0) where TEntity : ISettings, new();

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="storeId">Store identifier for which settings should be loaded.</param>
        /// <returns>The <see cref="ISettings"/>.</returns>
        ISettings LoadSetting(Type type, int storeId = 0);

        /// <summary>
        /// Save settings object.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="settings">Setting instance.</param>
        /// <param name="storeId">Store identifier.</param>
        void SaveSetting<TEntity>(TEntity settings, int storeId = 0) where TEntity : ISettings, new();

        /// <summary>
        /// Save settings object.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="storeId">Store ID.</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update.</param>
        void SaveSetting<TEntity, TPropType>(TEntity settings,
            Expression<Func<TEntity, TPropType>> keySelector,
            int storeId = 0, bool clearCache = true) where TEntity : ISettings, new();

        /// <summary>
        /// Save settings object (per store). If the setting is not overridden per store then it'll be delete.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="overrideForStore">A value indicating whether to setting is overridden in some store.</param>
        /// <param name="storeId">Store ID.</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update.</param>
        void SaveSettingOverridablePerStore<TEntity, TPropType>(TEntity settings,
            Expression<Func<TEntity, TPropType>> keySelector,
            bool overrideForStore, int storeId = 0, bool clearCache = true) where TEntity : ISettings, new();

        /// <summary>
        /// Delete all settings.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        void DeleteSetting<TEntity>() where TEntity : ISettings, new();

        /// <summary>
        /// Delete settings object.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TPropType">Property type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <param name="storeId">Store ID.</param>
        void DeleteSetting<TEntity, TPropType>(TEntity settings,
            Expression<Func<TEntity, TPropType>> keySelector, int storeId = 0) where TEntity : ISettings, new();

        /// <summary>
        /// Clear cache.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Get setting key (stored into database).
        /// </summary>
        /// <typeparam name="TSettings">Type of settings.</typeparam>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="settings">Settings.</param>
        /// <param name="keySelector">Key selector.</param>
        /// <returns>Key.</returns>
        string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector)
            where TSettings : ISettings, new();
    }
}
