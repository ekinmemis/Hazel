using System.Runtime.CompilerServices;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Provides access to the singleton instance of the Hazel engine.
    /// </summary>
    public class EngineContext
    {
        /// <summary>
        /// Create a static instance of the Hazel engine.
        /// </summary>
        /// <returns>The <see cref="IEngine"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create HazelEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new HazelEngine());
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        /// <summary>
        /// Gets the singleton Hazel engine used to access Hazel services..
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }
    }
}
