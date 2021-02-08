using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Security;
using Hazel.Data;
using Hazel.Data.Extensions;
using Hazel.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Security
{
    /// <summary>
    /// ACL service.
    /// </summary>
    public partial class AclService : IAclService
    {
        /// <summary>
        /// Defines the _eventPublisher.
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Defines the _aclRecordRepository.
        /// </summary>
        private readonly IRepository<AclRecord> _aclRecordRepository;

        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly IStaticCacheManager _cacheManager;

        /// <summary>
        /// Defines the _workContext.
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AclService"/> class.
        /// </summary>
        /// <param name="eventPublisher">The eventPublisher<see cref="IEventPublisher"/>.</param>
        /// <param name="aclRecordRepository">The aclRecordRepository<see cref="IRepository{AclRecord}"/>.</param>
        /// <param name="cacheManager">The cacheManager<see cref="IStaticCacheManager"/>.</param>
        /// <param name="workContext">The workContext<see cref="IWorkContext"/>.</param>
        public AclService(
            IEventPublisher eventPublisher,
            IRepository<AclRecord> aclRecordRepository,
            IStaticCacheManager cacheManager,
            IWorkContext workContext)
        {
            ;
            _eventPublisher = eventPublisher;
            _aclRecordRepository = aclRecordRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
        }

        /// <summary>
        /// Deletes an ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        public virtual void DeleteAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Delete(aclRecord);

            //cache
            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.AclRecordPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(aclRecord);
        }

        /// <summary>
        /// Gets an ACL record.
        /// </summary>
        /// <param name="aclRecordId">ACL record identifier.</param>
        /// <returns>ACL record.</returns>
        public virtual AclRecord GetAclRecordById(int aclRecordId)
        {
            if (aclRecordId == 0)
                return null;

            return _aclRecordRepository.GetById(aclRecordId);
        }

        /// <summary>
        /// Gets ACL records.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>ACL records.</returns>
        public virtual IList<AclRecord> GetAclRecords<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityId = entity.Id;
            var entityName = entity.GetUnproxiedEntityType().Name;

            var query = from ur in _aclRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName
                        select ur;
            var aclRecords = query.ToList();
            return aclRecords;
        }

        /// <summary>
        /// Inserts an ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        public virtual void InsertAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Insert(aclRecord);

            //cache
            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.AclRecordPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(aclRecord);
        }

        /// <summary>
        /// Inserts an ACL record.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="ApplicationUserRoleId">ApplicationUser role id.</param>
        public virtual void InsertAclRecord<TEntity>(TEntity entity, int ApplicationUserRoleId) where TEntity : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (ApplicationUserRoleId == 0)
                throw new ArgumentOutOfRangeException(nameof(ApplicationUserRoleId));

            var entityId = entity.Id;
            var entityName = entity.GetUnproxiedEntityType().Name;

            var aclRecord = new AclRecord
            {
                EntityId = entityId,
                EntityName = entityName,
                ApplicationUserRoleId = ApplicationUserRoleId
            };

            InsertAclRecord(aclRecord);
        }

        /// <summary>
        /// Updates the ACL record.
        /// </summary>
        /// <param name="aclRecord">ACL record.</param>
        public virtual void UpdateAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Update(aclRecord);

            //cache
            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.AclRecordPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(aclRecord);
        }

        /// <summary>
        /// Find ApplicationUser role identifiers with granted access.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>ApplicationUser role identifiers.</returns>
        public virtual int[] GetApplicationUserRoleIdsWithAccess<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityId = entity.Id;
            var entityName = entity.GetUnproxiedEntityType().Name;

            var key = string.Format(HazelSecurityDefaults.AclRecordByEntityIdNameCacheKey, entityId, entityName);
            return _cacheManager.Get(key, () =>
            {
                var query = from ur in _aclRecordRepository.Table
                            where ur.EntityId == entityId &&
                            ur.EntityName == entityName
                            select ur.ApplicationUserRoleId;
                return query.ToArray();
            });
        }

        /// <summary>
        /// Authorize ACL permission.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize<TEntity>(TEntity entity) where TEntity : BaseEntity, IAclSupported
        {
            return Authorize(entity, _workContext.CurrentApplicationUser);
        }

        /// <summary>
        /// Authorize ACL permission.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize<TEntity>(TEntity entity, ApplicationUser ApplicationUser) where TEntity : BaseEntity, IAclSupported
        {
            if (entity == null)
                return false;

            if (ApplicationUser == null)
                return false;


            if (!entity.SubjectToAcl)
                return true;

            foreach (var role1 in ApplicationUser.ApplicationUserRoles.Where(cr => cr.Active))
                foreach (var role2Id in GetApplicationUserRoleIdsWithAccess(entity))
                    if (role1.Id == role2Id)
                        //yes, we have such permission
                        return true;

            //no permission found
            return false;
        }
    }
}
