using System.Collections.Generic;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Provides a singleton list for a certain type.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class SingletonList<TEntity> : Singleton<IList<TEntity>>
    {
        /// <summary>
        /// Initializes static members of the <see cref="SingletonList{TEntity}"/> class.
        /// </summary>
        static SingletonList()
        {
            Singleton<IList<TEntity>>.Instance = new List<TEntity>();
        }

        /// <summary>
        /// Gets the Instance
        /// The singleton instance for the specified type T. Only one instance (at the time) of this list for each type of T..
        /// </summary>
        public static new IList<TEntity> Instance => Singleton<IList<TEntity>>.Instance;
    }
}
