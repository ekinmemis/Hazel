using Hazel.Core.Domain.Security;
using System.Collections.Generic;

namespace Hazel.Services.Security
{
    /// <summary>
    /// Permission provider.
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// Get permissions.
        /// </summary>
        /// <returns>Permissions.</returns>
        IEnumerable<PermissionRecord> GetPermissions();

        /// <summary>
        /// Get default permissions.
        /// </summary>
        /// <returns>Default permissions.</returns>
        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}
