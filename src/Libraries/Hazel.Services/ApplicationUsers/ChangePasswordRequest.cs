using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// Change password request.
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Gets or sets the Email
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ValidateRequest
        /// A value indicating whether we should validate request.
        /// </summary>
        public bool ValidateRequest { get; set; }

        /// <summary>
        /// Gets or sets the NewPasswordFormat
        /// Password format.
        /// </summary>
        public PasswordFormat NewPasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets the NewPassword
        /// New password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the OldPassword
        /// Old password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the HashedPasswordFormat
        /// Hashed password format (e.g. SHA1, SHA512).
        /// </summary>
        public string HashedPasswordFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordRequest"/> class.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="validateRequest">A value indicating whether we should validate request.</param>
        /// <param name="newPasswordFormat">Password format.</param>
        /// <param name="newPassword">New password.</param>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="hashedPasswordFormat">Hashed password format.</param>
        public ChangePasswordRequest(string email, bool validateRequest,
            PasswordFormat newPasswordFormat, string newPassword, string oldPassword = "",
            string hashedPasswordFormat = null)
        {
            Email = email;
            ValidateRequest = validateRequest;
            NewPasswordFormat = newPasswordFormat;
            NewPassword = newPassword;
            OldPassword = oldPassword;
            HashedPasswordFormat = hashedPasswordFormat;
        }
    }
}
