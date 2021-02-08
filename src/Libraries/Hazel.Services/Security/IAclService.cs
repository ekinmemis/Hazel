using Hazel.Core;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Security;
using System.Collections.Generic;

namespace Hazel.Services.Security
{
    /// <summary>
    /// ACL service interface.
    /// </summary>
    public partial interface IAclService
    {
        /// <summary>
        /// Deletes an ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        void DeleteAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Gets an ACL record.
        /// </summary>
        /// <param name="aclRecordId">ACL record identifier.</param>
        /// <returns>ACL record.</returns>
        AclRecord GetAclRecordById(int aclRecordId);

        /// <summary>
        /// Gets ACL records.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>ACL records.</returns>
        IList<AclRecord> GetAclRecords<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported;

        /// <summary>
        /// Inserts an ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        void InsertAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Inserts an ACL record.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="ApplicationUserRoleId">ApplicationUser role id.</param>
        void InsertAclRecord<TEntity>(TEntity entity, int ApplicationUserRoleId) where TEntity : BaseEntity, IAclSupported;

        /// <summary>
        /// Updates the ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        void UpdateAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Find ApplicationUser role identifiers with granted access.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>ApplicationUser role identifiers.</returns>
        int[] GetApplicationUserRoleIdsWithAccess<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported;

        /// <summary>
        /// Authorize ACL permission.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        bool Authorize<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported;

        /// <summary>
        /// Authorize ACL permission.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        bool Authorize<TEntity>(TEntity entity, ApplicationUser ApplicationUser) where TEntity : BaseEntity, IAclSupported;
    }
}
