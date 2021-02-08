using Hazel.Core.Caching;
using Hazel.Services.Tasks;

namespace Hazel.Services.Caching
{
    /// <summary>
    /// Clear cache scheduled task implementation.
    /// </summary>
    public partial class ClearCacheTask : IScheduleTask
    {
        /// <summary>
        /// Defines the _staticCacheManager.
        /// </summary>
        private readonly IStaticCacheManager _staticCacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearCacheTask"/> class.
        /// </summary>
        /// <param name="staticCacheManager">The staticCacheManager<see cref="IStaticCacheManager"/>.</param>
        public ClearCacheTask(IStaticCacheManager staticCacheManager)
        {
            _staticCacheManager = staticCacheManager;
        }

        /// <summary>
        /// Executes a task.
        /// </summary>
        public void Execute()
        {
            _staticCacheManager.Clear();
        }
    }
}
