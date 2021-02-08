using Hazel.Core.Domain.ApplicationUsers;
using System;

namespace Hazel.Core.Domain.Logging
{
    /// <summary>
    /// Represents an activity log record.
    /// </summary>
    public partial class ActivityLog : BaseEntity
    {
        /// <summary>
        /// Gets or sets the activity log type identifier.
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser identifier.
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the activity comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the ActivityLogType
        /// Gets the activity log type.
        /// </summary>
        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationUser
        /// Gets the applicationUser.
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public virtual string IpAddress { get; set; }
    }
}
