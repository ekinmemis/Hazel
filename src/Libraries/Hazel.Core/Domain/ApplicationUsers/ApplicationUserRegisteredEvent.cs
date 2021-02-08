namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser registered event.
    /// </summary>
    public class ApplicationUserRegisteredEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRegisteredEvent"/> class.
        /// </summary>
        /// <param name="applicationUser">applicationUser.</param>
        public ApplicationUserRegisteredEvent(ApplicationUser applicationUser)
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
