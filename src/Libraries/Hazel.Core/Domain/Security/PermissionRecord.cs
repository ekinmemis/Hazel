using System.Collections.Generic;

namespace Hazel.Core.Domain.Security
{
    /// <summary>
    /// Represents a permission record.
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {
        /// <summary>
        /// Defines the _permissionRecordApplicationUserRoleMappings.
        /// </summary>
        private ICollection<PermissionRecordApplicationUserRoleMapping> _permissionRecordApplicationUserRoleMappings;

        /// <summary>
        /// Gets or sets the permission name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the permission record-applicationUser role mappings.
        /// </summary>
        public virtual ICollection<PermissionRecordApplicationUserRoleMapping> PermissionRecordApplicationUserRoleMappings { get => _permissionRecordApplicationUserRoleMappings ?? (_permissionRecordApplicationUserRoleMappings = new List<PermissionRecordApplicationUserRoleMapping>()); protected set => _permissionRecordApplicationUserRoleMappings = value; }
    }
}
