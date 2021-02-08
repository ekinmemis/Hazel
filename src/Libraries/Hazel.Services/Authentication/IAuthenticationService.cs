using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Services.Authentication
{
    /// <summary>
    /// Authentication service interface.
    /// </summary>
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests.</param>
        void SignIn(ApplicationUser applicationUser, bool isPersistent);

        /// <summary>
        /// Sign out.
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated applicationUser.
        /// </summary>
        /// <returns>ApplicationUser.</returns>
        ApplicationUser GetAuthenticatedApplicationUser();
    }
}
