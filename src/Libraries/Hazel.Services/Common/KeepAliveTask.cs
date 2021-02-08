using Hazel.Services.Tasks;

namespace Hazel.Services.Common
{
    /// <summary>
    /// Represents a task for keeping the site alive.
    /// </summary>
    public partial class KeepAliveTask : IScheduleTask
    {
        /// <summary>
        /// Defines the _storeHttpClient.
        /// </summary>
        private readonly StoreHttpClient _storeHttpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveTask"/> class.
        /// </summary>
        /// <param name="storeHttpClient">The storeHttpClient<see cref="StoreHttpClient"/>.</param>
        public KeepAliveTask(StoreHttpClient storeHttpClient)
        {
            _storeHttpClient = storeHttpClient;
        }

        /// <summary>
        /// Executes a task.
        /// </summary>
        public void Execute()
        {
            _storeHttpClient.KeepAliveAsync().Wait();
        }
    }
}
