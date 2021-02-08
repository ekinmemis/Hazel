using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser registration request.
    /// </summary>
    public class ApplicationUserRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRegistrationRequest"/> class.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="email">Email.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="passwordFormat">Password format.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="isApproved">Is approved.</param>
        public ApplicationUserRegistrationRequest(ApplicationUser applicationUser, string email, string username,
            string password,
            PasswordFormat passwordFormat,
            int storeId,
            bool isApproved = true)
        {
            ApplicationUser = applicationUser;
            Email = email;
            Username = username;
            Password = password;
            PasswordFormat = passwordFormat;
            StoreId = storeId;
            IsApproved = isApproved;
        }

        /// <summary>
        /// Gets or sets the ApplicationUser
        /// ApplicationUser.
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Username
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the PasswordFormat
        /// Password format.
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets the StoreId
        /// Store identifier.
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsApproved
        /// Is approved.
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
