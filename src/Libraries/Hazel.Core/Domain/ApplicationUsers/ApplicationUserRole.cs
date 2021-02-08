using Hazel.Core.Domain.Security;
using System.Collections.Generic;

namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser role.
    /// </summary>
    public partial class ApplicationUserRole : BaseEntity
    {
        /// <summary>
        /// Defines the _permissionRecordApplicationUserRoleMappings.
        /// </summary>
        private ICollection<PermissionRecordApplicationUserRoleMapping> _permissionRecordApplicationUserRoleMappings;

        /// <summary>
        /// Gets or sets the applicationUser role name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser role is marked as free shipping.
        /// </summary>
        public bool FreeShipping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser role is marked as tax exempt.
        /// </summary>
        public bool TaxExempt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser role is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser role is system.
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role system name.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUsers must change passwords after a specified time.
        /// </summary>
        public bool EnablePasswordLifetime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUsers of this role have other tax display type chosen instead of the default one.
        /// </summary>
        public bool OverrideTaxDisplayType { get; set; }

        /// <summary>
        /// Gets or sets the DefaultTaxDisplayTypeId
        /// Gets or sets identifier of the default tax display type (used only with "OverrideTaxDisplayType" enabled).
        /// </summary>
        public int DefaultTaxDisplayTypeId { get; set; }

        /// <summary>
        /// Gets or sets the PurchasedWithProductId
        /// Gets or sets a product identifier that is required by this applicationUser role. 
        /// A applicationUser is added to this applicationUser role once a specified product is purchased..
        /// </summary>
        public int PurchasedWithProductId { get; set; }

        /// <summary>
        /// Gets or sets the permission record-applicationUser role mappings.
        /// </summary>
        public virtual ICollection<PermissionRecordApplicationUserRoleMapping> PermissionRecordApplicationUserRoleMappings { get => _permissionRecordApplicationUserRoleMappings ?? (_permissionRecordApplicationUserRoleMappings = new List<PermissionRecordApplicationUserRoleMapping>()); protected set => _permissionRecordApplicationUserRoleMappings = value; }
    }
}
