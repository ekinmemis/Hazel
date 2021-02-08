using Hazel.Core.Caching;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Services.Events;

namespace Hazel.Services.ApplicationUsers.Cache
{
    /// <summary>
    /// ApplicationUser cache event consumer (used for caching of current applicationUser password).
    /// </summary>
    public partial class ApplicationUserCacheEventConsumer : IConsumer<ApplicationUserPasswordChangedEvent>
    {
        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly IStaticCacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserCacheEventConsumer"/> class.
        /// </summary>
        /// <param name="cacheManager">The cacheManager<see cref="IStaticCacheManager"/>.</param>
        public ApplicationUserCacheEventConsumer(IStaticCacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        //password changed
        /// <summary>
        /// The HandleEvent.
        /// </summary>
        /// <param name="eventMessage">The eventMessage<see cref="ApplicationUserPasswordChangedEvent"/>.</param>
        public void HandleEvent(ApplicationUserPasswordChangedEvent eventMessage)
        {
            _cacheManager.Remove(string.Format(HazelApplicationUserServiceDefaults.ApplicationUserPasswordLifetimeCacheKey, eventMessage.Password.ApplicationUserId));
        }
    }
}
