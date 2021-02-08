using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// Email account settings.
    /// </summary>
    public class EmailAccountSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the DefaultEmailAccountId
        /// Gets or sets a store default email account identifier.
        /// </summary>
        public int DefaultEmailAccountId { get; set; }
    }
}
