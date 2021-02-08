using Hazel.Core.Domain.ApplicationUsers;

namespace Hazel.Core.Domain.Security
{
    /// <summary>
    /// Represents a permission record-applicationUser role mapping class.
    /// </summary>
    public partial class PermissionRecordApplicationUserRoleMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the permission record identifier.
        /// </summary>
        public int PermissionRecordId { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role identifier.
        /// </summary>
        public int ApplicationUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the permission record.
        /// </summary>
        public virtual PermissionRecord PermissionRecord { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role.
        /// </summary>
        public virtual ApplicationUserRole ApplicationUserRole { get; set; }
    }
}
