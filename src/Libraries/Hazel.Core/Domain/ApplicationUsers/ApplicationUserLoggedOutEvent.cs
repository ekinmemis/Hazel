namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// "ApplicationUser is logged out" event.
    /// </summary>
    public class ApplicationUserLoggedOutEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserLoggedOutEvent"/> class.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        public ApplicationUserLoggedOutEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        /// <summary>
        /// Gets the ApplicationUser
        /// Get or set the applicationUser.
        /// </summary>
        public ApplicationUser ApplicationUser { get; }
    }
}
