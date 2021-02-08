using Hazel.Core.Domain.Common;
using Hazel.Core.Domain.Tasks;
using Hazel.Core.Http;
using Hazel.Core.Infrastructure;
using Hazel.Services.Localization;
using Hazel.Services.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hazel.Services.Tasks
{
    /// <summary>
    /// Represents task thread.
    /// </summary>
    public partial class TaskThread : IDisposable
    {
        /// <summary>
        /// Defines the _scheduleTaskUrl.
        /// </summary>
        private static readonly string _scheduleTaskUrl;

        /// <summary>
        /// Defines the _timeout.
        /// </summary>
        private static readonly int? _timeout;

        /// <summary>
        /// Defines the _tasks.
        /// </summary>
        private readonly Dictionary<string, string> _tasks;

        /// <summary>
        /// Defines the _timer.
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Defines the _disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes static members of the <see cref="TaskThread"/> class.
        /// </summary>
        static TaskThread()
        {
            _timeout = EngineContext.Current.Resolve<CommonSettings>().ScheduleTaskRunTimeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskThread"/> class.
        /// </summary>
        internal TaskThread()
        {
            _tasks = new Dictionary<string, string>();
            Seconds = 10 * 60;
        }

        /// <summary>
        /// The Run.
        /// </summary>
        private void Run()
        {
            if (Seconds <= 0)
                return;

            StartedUtc = DateTime.UtcNow;
            IsRunning = true;

            foreach (var taskName in _tasks.Keys)
            {
                var taskType = _tasks[taskName];
                try
                {
                    //create and configure client
                    var client = EngineContext.Current.Resolve<IHttpClientFactory>().CreateClient(HazelHttpDefaults.DefaultHttpClient);
                    if (_timeout.HasValue)
                        client.Timeout = TimeSpan.FromMilliseconds(_timeout.Value);

                    //send post data
                    var data = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(nameof(taskType), taskType) });
                    client.PostAsync(ScheduleTaskUrl1, data).Wait();
                }
                catch (Exception ex)
                {
                    var _serviceScopeFactory = EngineContext.Current.Resolve<IServiceScopeFactory>();
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        // Resolve
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                        var localizationService = scope.ServiceProvider.GetRequiredService<ILocalizationService>();

                        var message = ex.InnerException?.GetType() == typeof(TaskCanceledException) ? localizationService.GetResource("ScheduleTasks.TimeoutError") : ex.Message;

                        message = string.Format(localizationService.GetResource("ScheduleTasks.Error"), taskName,
                            message, taskType, ScheduleTaskUrl1);

                        logger.Error(message, ex);
                    }
                }
            }

            IsRunning = false;
        }

        /// <summary>
        /// The TimerHandler.
        /// </summary>
        /// <param name="state">The state<see cref="object"/>.</param>
        private void TimerHandler(object state)
        {
            try
            {
                _timer.Change(-1, -1);
                Run();

                if (RunOnlyOnce)
                    Dispose();
                else
                    _timer.Change(Interval, Interval);
            }
            catch
            {
                // ignore
            }
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public void Dispose()
        {
            if (_timer == null || _disposed)
                return;

            lock (this)
            {
                _timer.Dispose();
                _timer = null;
                _disposed = true;
            }
        }

        /// <summary>
        /// Inits a timer.
        /// </summary>
        public void InitTimer()
        {
            if (_timer == null)
                _timer = new Timer(TimerHandler, null, InitInterval, Interval);
        }

        /// <summary>
        /// Adds a task to the thread.
        /// </summary>
        /// <param name="task">The task to be added.</param>
        public void AddTask(ScheduleTask task)
        {
            if (!_tasks.ContainsKey(task.Name))
                _tasks.Add(task.Name, task.Type);
        }

        /// <summary>
        /// Gets or sets the interval in seconds at which to run the tasks.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Gets or sets the InitSeconds
        /// Get or set the interval before timer first start.
        /// </summary>
        public int InitSeconds { get; set; }

        /// <summary>
        /// Gets the StartedUtc
        /// Get or sets a datetime when thread has been started.
        /// </summary>
        public DateTime StartedUtc { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsRunning
        /// Get or sets a value indicating whether thread is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the interval (in milliseconds) at which to run the task.
        /// </summary>
        public int Interval
        {
            get
            {
                //if somebody entered more than "2147483" seconds, then an exception could be thrown (exceeds int.MaxValue)
                var interval = Seconds * 1000;
                if (interval <= 0)
                    interval = int.MaxValue;
                return interval;
            }
        }

        /// <summary>
        /// Gets the due time interval (in milliseconds) at which to begin start the task.
        /// </summary>
        public int InitInterval
        {
            get
            {
                //if somebody entered less than "0" seconds, then an exception could be thrown
                var interval = InitSeconds * 1000;
                if (interval <= 0)
                    interval = 0;
                return interval;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the thread would be run only once (on application start).
        /// </summary>
        public bool RunOnlyOnce { get; set; }

        /// <summary>
        /// Gets the ScheduleTaskUrl.
        /// </summary>
        public static string ScheduleTaskUrl => ScheduleTaskUrl1;

        public static string ScheduleTaskUrl1 => _scheduleTaskUrl;
    }
}
