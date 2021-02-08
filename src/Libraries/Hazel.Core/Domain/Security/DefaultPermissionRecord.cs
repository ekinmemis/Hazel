using System.Collections.Generic;

namespace Hazel.Core.Domain.Security
{
    /// <summary>
    /// Represents a default permission record.
    /// </summary>
    public class DefaultPermissionRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPermissionRecord"/> class.
        /// </summary>
        public DefaultPermissionRecord()
        {
            PermissionRecords = new List<PermissionRecord>();
        }

        /// <summary>
        /// Gets or sets the applicationUser role system name.
        /// </summary>
        public string ApplicationUserRoleSystemName { get; set; }

        /// <summary>
        /// Gets or sets the PermissionRecords.
        /// </summary>
        public IEnumerable<PermissionRecord> PermissionRecords { get; set; }
    }
}
