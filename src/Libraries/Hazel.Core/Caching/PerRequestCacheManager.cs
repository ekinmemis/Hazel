using Hazel.Core.ComponentModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hazel.Core.Caching
{
    /// <summary>
    /// Represents a manager for caching during an HTTP request (short term caching).
    /// </summary>
    public partial class PerRequestCacheManager : ICacheManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerRequestCacheManager"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
        public PerRequestCacheManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _locker = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Gets a key/value collection that can be used to share data within the scope of this request.
        /// </summary>
        /// <returns>The <see cref="IDictionary{object, object}"/>.</returns>
        protected virtual IDictionary<object, object> GetItems()
        {
            return _httpContextAccessor.HttpContext?.Items;
        }

        /// <summary>
        /// Defines the _httpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Defines the _locker.
        /// </summary>
        private readonly ReaderWriterLockSlim _locker;

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet.</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time.</param>
        /// <returns>The cached value associated with the specified key.</returns>
        public virtual TEntity Get<TEntity>(string key, Func<TEntity> acquire, int? cacheTime = null)
        {
            IDictionary<object, object> items;

            using (new ReaderWriteLockDisposable(_locker, ReaderWriteLockType.Read))
            {
                items = GetItems();
                if (items == null)
                    return acquire();

                //item already is in cache, so return it
                if (items[key] != null)
                    return (TEntity)items[key];
            }

            //or create it using passed function
            var result = acquire();

            if (result == null || (cacheTime ?? HazelCachingDefaults.CacheTime) <= 0)
                return result;

            //and set in cache (if cache time is defined)
            using (new ReaderWriteLockDisposable(_locker))
            {
                items[key] = result;
            }

            return result;
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        /// <param name="data">Value for caching.</param>
        /// <param name="cacheTime">Cache time in minutes.</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            using (new ReaderWriteLockDisposable(_locker))
            {
                var items = GetItems();
                if (items == null)
                    return;

                items[key] = data;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        /// <returns>True if item already is in cache; otherwise false.</returns>
        public virtual bool IsSet(string key)
        {
            using (new ReaderWriteLockDisposable(_locker, ReaderWriteLockType.Read))
            {
                var items = GetItems();
                return items?[key] != null;
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache.
        /// </summary>
        /// <param name="key">Key of cached item.</param>
        public virtual void Remove(string key)
        {
            using (new ReaderWriteLockDisposable(_locker))
            {
                var items = GetItems();
                items?.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by key prefix.
        /// </summary>
        /// <param name="prefix">String key prefix.</param>
        public virtual void RemoveByPrefix(string prefix)
        {
            using (new ReaderWriteLockDisposable(_locker, ReaderWriteLockType.UpgradeableRead))
            {
                var items = GetItems();
                if (items == null)
                    return;

                //get cache keys that matches pattern
                var regex = new Regex(prefix,
                    RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var matchesKeys = items.Keys.Select(p => p.ToString()).Where(key => regex.IsMatch(key)).ToList();

                if (!matchesKeys.Any())
                    return;

                using (new ReaderWriteLockDisposable(_locker))
                {
                    //remove matching values
                    foreach (var key in matchesKeys)
                    {
                        items.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// Clear all cache data.
        /// </summary>
        public virtual void Clear()
        {
            using (new ReaderWriteLockDisposable(_locker))
            {
                var items = GetItems();
                items?.Clear();
            }
        }

        /// <summary>
        /// Dispose cache manager.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
