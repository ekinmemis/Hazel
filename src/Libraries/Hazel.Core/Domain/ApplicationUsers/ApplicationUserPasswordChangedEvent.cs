namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser password changed event.
    /// </summary>
    public class ApplicationUserPasswordChangedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserPasswordChangedEvent"/> class.
        /// </summary>
        /// <param name="password">Password.</param>
        public ApplicationUserPasswordChangedEvent(ApplicationUserPassword password)
        {
            Password = password;
        }

        /// <summary>
        /// Gets the Password
        /// ApplicationUser password.
        /// </summary>
        public ApplicationUserPassword Password { get; }
    }
}
