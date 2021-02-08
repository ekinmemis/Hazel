using System;
using System.Collections.Generic;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Provides access to all "singletons" stored by <see cref="Singleton{T}"/>.
    /// </summary>
    public class BaseSingleton
    {
        /// <summary>
        /// Initializes static members of the <see cref="BaseSingleton"/> class.
        /// </summary>
        static BaseSingleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the AllSingletons
        /// Dictionary of type to singleton instances..
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }
    }
}
