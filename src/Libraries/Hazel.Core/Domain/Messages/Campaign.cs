using System;

namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// Represents a campaign.
    /// </summary>
    public partial class Campaign : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the store identifier  which subscribers it will be sent to; set 0 for all newsletter subscribers.
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role identifier  which subscribers it will be sent to; set 0 for all newsletter subscribers.
        /// </summary>
        public int ApplicationUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time in UTC before which this email should not be sent.
        /// </summary>
        public DateTime? DontSendBeforeDateUtc { get; set; }
    }
}
