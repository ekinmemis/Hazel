using System;

namespace Hazel.Core.Caching
{
    /// <summary>
    /// Cache manager interface.
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet.</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time.</param>
        /// <returns>The cached value associated with the specified key.</returns>
        TEntity Get<TEntity>(string key, Func<TEntity> acquire, int? cacheTime = null);

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        /// <param name="data">Value for caching.</param>
        /// <param name="cacheTime">Cache time in minutes.</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        /// <returns>True if item already is in cache; otherwise false.</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        void Remove(string key);

        /// <summary>
        /// Removes items by key prefix.
        /// </summary>
        /// <param name="prefix">String key prefix.</param>
        void RemoveByPrefix(string prefix);

        /// <summary>
        /// Clear all cache data.
        /// </summary>
        void Clear();
    }
}
