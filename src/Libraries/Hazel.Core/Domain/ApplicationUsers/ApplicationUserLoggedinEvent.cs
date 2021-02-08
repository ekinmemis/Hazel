namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser logged-in event.
    /// </summary>
    public class ApplicationUserLoggedinEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserLoggedinEvent"/> class.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        public ApplicationUserLoggedinEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        /// <summary>
        /// Gets the ApplicationUser
        /// ApplicationUser.
        /// </summary>
        public ApplicationUser ApplicationUser { get; }
    }
}
