using System.Collections.Generic;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Provides a singleton dictionary for a certain key and vlaue type.
    /// </summary>
    /// <typeparam name="TKey">The type of key.</typeparam>
    /// <typeparam name="TValue">The type of value.</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        /// <summary>
        /// Initializes static members of the <see cref="SingletonDictionary{TKey, TValue}"/> class.
        /// </summary>
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Gets the Instance
        /// The singleton instance for the specified type T. Only one instance (at the time) of this dictionary for each type of T..
        /// </summary>
        public static new IDictionary<TKey, TValue> Instance => Singleton<Dictionary<TKey, TValue>>.Instance;
    }
}
