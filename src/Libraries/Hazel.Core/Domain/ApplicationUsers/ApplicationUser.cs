using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser.
    /// </summary>
    public partial class ApplicationUser : BaseEntity
    {
        /// <summary>
        /// Defines the _applicationUserApplicationUserRoleMappings.
        /// </summary>
        private ICollection<ApplicationUserApplicationUserRoleMapping> _applicationUserApplicationUserRoleMappings;

        /// <summary>
        /// Defines the _applicationUserRoles.
        /// </summary>
        private IList<ApplicationUserRole> _applicationUserRoles;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
        /// </summary>
        public ApplicationUser()
        {
            ApplicationUserGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the applicationUser GUID.
        /// </summary>
        public Guid ApplicationUserGuid { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the email that should be re-validated. Used in scenarios when a applicationUser is already registered and wants to change an email address..
        /// </summary>
        public string EmailToRevalidate { get; set; }

        /// <summary>
        /// Gets or sets the admin comment.
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser is tax exempt.
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier.
        /// </summary>
        public int AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the vendor identifier with which this applicationUser is associated (maganer).
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this applicationUser has some products in the shopping cart
        /// <remarks>The same as if we run ShoppingCartItems.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load "ShoppingCartItems" navigation property for each page load
        /// It's used only in a couple of places in the presenation layer
        /// </remarks>.
        /// </summary>
        public bool HasShoppingCartItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser is required to re-login.
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets the FailedLoginAttempts
        /// Gets or sets a value indicating number of failed login attempts (wrong password).
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a applicationUser cannot login (locked out).
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser has been deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the applicationUser account is system.
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser system name.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the last IP address.
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login.
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity.
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the RegisteredInStoreId
        /// Gets or sets the store identifier in which applicationUser registered.
        /// </summary>
        public int RegisteredInStoreId { get; set; }

        /// <summary>
        /// Gets or sets the billing address identifier.
        /// </summary>
        public int? BillingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier.
        /// </summary>
        public int? ShippingAddressId { get; set; }

        /// <summary>
        /// Gets the ApplicationUserRoles
        /// Gets or sets applicationUser roles.
        /// </summary>
        public virtual IList<ApplicationUserRole> ApplicationUserRoles { get => _applicationUserRoles ?? (_applicationUserRoles = ApplicationUserApplicationUserRoleMappings.Select(mapping => mapping.ApplicationUserRole).ToList()); }

        /// <summary>
        /// Gets or sets the ApplicationUserApplicationUserRoleMappings
        /// Gets or sets applicationUser-applicationUser role mappings.
        /// </summary>
        public virtual ICollection<ApplicationUserApplicationUserRoleMapping> ApplicationUserApplicationUserRoleMappings { get => _applicationUserApplicationUserRoleMappings ?? (_applicationUserApplicationUserRoleMappings = new List<ApplicationUserApplicationUserRoleMapping>()); protected set => _applicationUserApplicationUserRoleMappings = value; }

        /// <summary>
        /// Add applicationUser role and reset applicationUser roles cache.
        /// </summary>
        /// <param name="role">Role.</param>
        public void AddApplicationUserRoleMapping(ApplicationUserApplicationUserRoleMapping role)
        {
            ApplicationUserApplicationUserRoleMappings.Add(role);
            _applicationUserRoles = null;
        }

        /// <summary>
        /// Remove applicationUser role and reset applicationUser roles cache.
        /// </summary>
        /// <param name="role">Role.</param>
        public void RemoveApplicationUserRoleMapping(ApplicationUserApplicationUserRoleMapping role)
        {
            ApplicationUserApplicationUserRoleMappings.Remove(role);
            _applicationUserRoles = null;
        }
    }
}
