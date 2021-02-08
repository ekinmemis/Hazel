using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Core.Domain.Security
{
    /// <summary>
    /// Represents an ACL record.
    /// </summary>
    public partial class AclRecord : BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role identifier.
        /// </summary>
        public int ApplicationUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role.
        /// </summary>
        public virtual ApplicationUserRole ApplicationUserRole { get; set; }
    }
}