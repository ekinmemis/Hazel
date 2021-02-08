using System;

namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser password.
    /// </summary>
    public partial class ApplicationUserPassword : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserPassword"/> class.
        /// </summary>
        public ApplicationUserPassword()
        {
            PasswordFormat = PasswordFormat.Clear;
        }

        /// <summary>
        /// Gets or sets the applicationUser identifier.
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password format identifier.
        /// </summary>
        public int PasswordFormatId { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the password format.
        /// </summary>
        public PasswordFormat PasswordFormat { get => (PasswordFormat)PasswordFormatId; set => PasswordFormatId = (int)value; }

        /// <summary>
        /// Gets or sets the ApplicationUser.
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
