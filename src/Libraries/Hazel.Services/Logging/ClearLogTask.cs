using Hazel.Services.Tasks;

namespace Hazel.Services.Logging
{
    /// <summary>
    /// Represents a task to clear [Log] table.
    /// </summary>
    public partial class ClearLogTask : IScheduleTask
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearLogTask"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        public ClearLogTask(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Executes a task.
        /// </summary>
        public virtual void Execute()
        {
            _logger.ClearLog();
        }
    }
}
