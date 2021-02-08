namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// Represents an email account.
    /// </summary>
    public partial class EmailAccount : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Email
        /// Gets or sets an email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the DisplayName
        /// Gets or sets an email display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the Host
        /// Gets or sets an email host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the Port
        /// Gets or sets an email port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the Username
        /// Gets or sets an email user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// Gets or sets an email password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EnableSsl
        /// Gets or sets a value that controls whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseDefaultCredentials
        /// Gets or sets a value that controls whether the default system credentials of the application are sent with requests..
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets the FriendlyName
        /// Gets a friendly email account name.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DisplayName))
                    return Email + " (" + DisplayName + ")";

                return Email;
            }
        }
    }
}
