using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser registration interface.
    /// </summary>
    public partial interface IApplicationUserRegistrationService
    {
        /// <summary>
        /// Validate ApplicationUser.
        /// </summary>
        /// <param name="usernameOrEmail">Username or email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Result.</returns>
        ApplicationUserLoginResults ValidateApplicationUser(string usernameOrEmail, string password);

        /// <summary>
        /// Register ApplicationUser.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Result.</returns>
        ApplicationUserRegistrationResult RegisterApplicationUser(ApplicationUserRegistrationRequest request);

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns>Result.</returns>
        ChangePasswordResult ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Sets a user email.
        /// </summary>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <param name="newEmail">New email.</param>
        /// <param name="requireValidation">Require validation of new email address.</param>
        void SetEmail(ApplicationUser ApplicationUser, string newEmail, bool requireValidation);

        /// <summary>
        /// Sets a ApplicationUser username.
        /// </summary>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <param name="newUsername">New Username.</param>
        void SetUsername(ApplicationUser ApplicationUser, string newUsername);
    }
}
