using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Services.Tasks;
using System;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// Represents a task for deleting guest applicationUsers.
    /// </summary>
    public partial class DeleteGuestsTask : IScheduleTask
    {
        /// <summary>
        /// Defines the _applicationUserSettings.
        /// </summary>
        private readonly ApplicationUserSettings _applicationUserSettings;

        /// <summary>
        /// Defines the _applicationUserService.
        /// </summary>
        private readonly IApplicationUserService _applicationUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGuestsTask"/> class.
        /// </summary>
        /// <param name="applicationUserSettings">The applicationUserSettings<see cref="ApplicationUserSettings"/>.</param>
        /// <param name="applicationUserService">The applicationUserService<see cref="IApplicationUserService"/>.</param>
        public DeleteGuestsTask(ApplicationUserSettings applicationUserSettings,
            IApplicationUserService applicationUserService)
        {
            _applicationUserSettings = applicationUserSettings;
            _applicationUserService = applicationUserService;
        }

        /// <summary>
        /// Executes a task.
        /// </summary>
        public void Execute()
        {
            var olderThanMinutes = _applicationUserSettings.DeleteGuestTaskOlderThanMinutes;
            // Default value in case 0 is returned.  0 would effectively disable this service and harm performance.
            olderThanMinutes = olderThanMinutes == 0 ? 1440 : olderThanMinutes;

            _applicationUserService.DeleteGuestApplicationUsers(null, DateTime.UtcNow.AddMinutes(-olderThanMinutes), true);
        }
    }
}
