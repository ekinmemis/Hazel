using Hazel.Core;
using Hazel.Core.Caching;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Security;
using Hazel.Data;
using Hazel.Services.ApplicationUsers;
using Hazel.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Security
{
    /// <summary>
    /// Permission service.
    /// </summary>
    public partial class PermissionService : IPermissionService
    {
        /// <summary>
        /// Defines the _cacheManager.
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Defines the _ApplicationUserService.
        /// </summary>
        private readonly IApplicationUserService _ApplicationUserService;

        /// <summary>
        /// Defines the _localizationService.
        /// </summary>
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Defines the _permissionRecordRepository.
        /// </summary>
        private readonly IRepository<PermissionRecord> _permissionRecordRepository;

        /// <summary>
        /// Defines the _permissionRecordApplicationUserRoleMappingRepository.
        /// </summary>
        private readonly IRepository<PermissionRecordApplicationUserRoleMapping> _permissionRecordApplicationUserRoleMappingRepository;

        /// <summary>
        /// Defines the _staticCacheManager.
        /// </summary>
        private readonly IStaticCacheManager _staticCacheManager;

        /// <summary>
        /// Defines the _workContext.
        /// </summary>
        private readonly IWorkContext _workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="cacheManager">The cacheManager<see cref="ICacheManager"/>.</param>
        /// <param name="ApplicationUserService">The ApplicationUserService<see cref="IApplicationUserService"/>.</param>
        /// <param name="localizationService">The localizationService<see cref="ILocalizationService"/>.</param>
        /// <param name="permissionRecordRepository">The permissionRecordRepository<see cref="IRepository{PermissionRecord}"/>.</param>
        /// <param name="permissionRecordApplicationUserRoleMappingRepository">The permissionRecordApplicationUserRoleMappingRepository<see cref="IRepository{PermissionRecordApplicationUserRoleMapping}"/>.</param>
        /// <param name="staticCacheManager">The staticCacheManager<see cref="IStaticCacheManager"/>.</param>
        /// <param name="workContext">The workContext<see cref="IWorkContext"/>.</param>
        public PermissionService(ICacheManager cacheManager,
            IApplicationUserService ApplicationUserService,
            ILocalizationService localizationService,
            IRepository<PermissionRecord> permissionRecordRepository,
            IRepository<PermissionRecordApplicationUserRoleMapping> permissionRecordApplicationUserRoleMappingRepository,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _cacheManager = cacheManager;
            _ApplicationUserService = ApplicationUserService;
            _localizationService = localizationService;
            _permissionRecordRepository = permissionRecordRepository;
            _permissionRecordApplicationUserRoleMappingRepository = permissionRecordApplicationUserRoleMappingRepository;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }

        /// <summary>
        /// Get permission records by ApplicationUser role identifier.
        /// </summary>
        /// <param name="ApplicationUserRoleId">ApplicationUser role identifier.</param>
        /// <returns>Permissions.</returns>
        protected virtual IList<PermissionRecord> GetPermissionRecordsByApplicationUserRoleId(int ApplicationUserRoleId)
        {
            var key = string.Format(HazelSecurityDefaults.PermissionsAllByApplicationUserRoleIdCacheKey, ApplicationUserRoleId);
            return _cacheManager.Get(key, () =>
            {
                var query = from pr in _permissionRecordRepository.Table
                            join prcrm in _permissionRecordApplicationUserRoleMappingRepository.Table on pr.Id equals prcrm.PermissionRecordId
                            where prcrm.ApplicationUserRoleId == ApplicationUserRoleId
                            orderby pr.Id
                            select pr;

                return query.ToList();
            });
        }

        /// <summary>
        /// Authorize permission.
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name.</param>
        /// <param name="ApplicationUserRoleId">ApplicationUser role identifier.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, int ApplicationUserRoleId)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var key = string.Format(HazelSecurityDefaults.PermissionsAllowedCacheKey, ApplicationUserRoleId, permissionRecordSystemName);
            return _staticCacheManager.Get(key, () =>
            {
                var permissions = GetPermissionRecordsByApplicationUserRoleId(ApplicationUserRoleId);
                foreach (var permission1 in permissions)
                    if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }

        /// <summary>
        /// Delete a permission.
        /// </summary>
        /// <param name="permission">Permission.</param>
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Delete(permission);

            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
            _staticCacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
        }

        /// <summary>
        /// Gets a permission.
        /// </summary>
        /// <param name="permissionId">Permission identifier.</param>
        /// <returns>Permission.</returns>
        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        /// <summary>
        /// Gets a permission.
        /// </summary>
        /// <param name="systemName">Permission system name.</param>
        /// <returns>Permission.</returns>
        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <returns>Permissions.</returns>
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        /// <summary>
        /// Inserts a permission.
        /// </summary>
        /// <param name="permission">Permission.</param>
        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Insert(permission);

            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
            _staticCacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
        }

        /// <summary>
        /// Updates the permission.
        /// </summary>
        /// <param name="permission">Permission.</param>
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);

            _cacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
            _staticCacheManager.RemoveByPrefix(HazelSecurityDefaults.PermissionsPrefixCacheKey);
        }

        /// <summary>
        /// Install permissions.
        /// </summary>
        /// <param name="permissionProvider">Permission provider.</param>
        public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        {
            //install new permissions
            var permissions = permissionProvider.GetPermissions();
            //default ApplicationUser role mappings
            var defaultPermissions = permissionProvider.GetDefaultPermissions().ToList();

            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 != null)
                    continue;

                //new permission (install it)
                permission1 = new PermissionRecord
                {
                    Name = permission.Name,
                    SystemName = permission.SystemName,
                    Category = permission.Category
                };

                foreach (var defaultPermission in defaultPermissions)
                {
                    var ApplicationUserRole = _ApplicationUserService.GetApplicationUserRoleBySystemName(defaultPermission.ApplicationUserRoleSystemName);
                    if (ApplicationUserRole == null)
                    {
                        //new role (save it)
                        ApplicationUserRole = new ApplicationUserRole
                        {
                            Name = defaultPermission.ApplicationUserRoleSystemName,
                            Active = true,
                            SystemName = defaultPermission.ApplicationUserRoleSystemName
                        };
                        _ApplicationUserService.InsertApplicationUserRole(ApplicationUserRole);
                    }

                    var defaultMappingProvided = (from p in defaultPermission.PermissionRecords
                                                  where p.SystemName == permission1.SystemName
                                                  select p).Any();
                    var mappingExists = (from mapping in ApplicationUserRole.PermissionRecordApplicationUserRoleMappings
                                         where mapping.PermissionRecord.SystemName == permission1.SystemName
                                         select mapping.PermissionRecord).Any();
                    if (defaultMappingProvided && !mappingExists)
                    {
                        //permission1.ApplicationUserRoles.Add(ApplicationUserRole);
                        permission1.PermissionRecordApplicationUserRoleMappings.Add(new PermissionRecordApplicationUserRoleMapping { ApplicationUserRole = ApplicationUserRole });
                    }
                }

                //save new permission
                InsertPermissionRecord(permission1);

                //save localization
                _localizationService.SaveLocalizedPermissionName(permission1);
            }
        }

        /// <summary>
        /// Uninstall permissions.
        /// </summary>
        /// <param name="permissionProvider">Permission provider.</param>
        public virtual void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 == null)
                    continue;

                DeletePermissionRecord(permission1);

                //delete permission locales
                _localizationService.DeleteLocalizedPermissionName(permission1);
            }
        }

        /// <summary>
        /// Authorize permission.
        /// </summary>
        /// <param name="permission">Permission record.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentApplicationUser);
        }

        /// <summary>
        /// Authorize permission.
        /// </summary>
        /// <param name="permission">Permission record.</param>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize(PermissionRecord permission, ApplicationUser ApplicationUser)
        {
            if (permission == null)
                return false;

            if (ApplicationUser == null)
                return false;

            //old implementation of Authorize method
            //var ApplicationUserRoles = ApplicationUser.ApplicationUserRoles.Where(cr => cr.Active);
            //foreach (var role in ApplicationUserRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;

            //return false;

            return Authorize(permission.SystemName, ApplicationUser);
        }

        /// <summary>
        /// Authorize permission.
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentApplicationUser);
        }

        /// <summary>
        /// Authorize permission.
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name.</param>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <returns>true - authorized; otherwise, false.</returns>
        public virtual bool Authorize(string permissionRecordSystemName, ApplicationUser ApplicationUser)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var ApplicationUserRoles = ApplicationUser.ApplicationUserRoles.Where(cr => cr.Active);
            foreach (var role in ApplicationUserRoles)
                if (Authorize(permissionRecordSystemName, role.Id))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
    }
}
