namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// A statically compiled "singleton" used to store objects throughout the 
    /// lifetime of the app domain. Not so much singleton in the pattern's 
    /// sense of the word as a standardized way to store single instances.
    /// </summary>
    /// <typeparam name="TEntity">.</typeparam>
    public class Singleton<TEntity> : BaseSingleton
    {
        /// <summary>
        /// Defines the instance.
        /// </summary>
        private static TEntity instance;

        /// <summary>
        /// Gets or sets the Instance
        /// The singleton instance for the specified type T. Only one instance (at the time) of this object for each type of T..
        /// </summary>
        public static TEntity Instance
        {
            get => instance;
            set
            {
                instance = value;
                AllSingletons[typeof(TEntity)] = value;
            }
        }
    }
}
