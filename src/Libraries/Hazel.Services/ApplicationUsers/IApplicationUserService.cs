using Hazel.Core;
using Hazel.Core.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser service interface.
    /// </summary>
    public partial interface IApplicationUserService
    {
        /// <summary>
        /// Gets all applicationUsers.
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records.</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records.</param>
        /// <param name="affiliateId">Affiliate identifier.</param>
        /// <param name="vendorId">Vendor identifier.</param>
        /// <param name="applicationUserRoleIds">A list of applicationUser role identifiers to filter by (at least one match); pass null or empty list in order to load all applicationUsers; .</param>
        /// <param name="email">Email; null to load all applicationUsers.</param>
        /// <param name="username">Username; null to load all applicationUsers.</param>
        /// <param name="firstName">First name; null to load all applicationUsers.</param>
        /// <param name="lastName">Last name; null to load all applicationUsers.</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all applicationUsers.</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all applicationUsers.</param>
        /// <param name="company">Company; null to load all applicationUsers.</param>
        /// <param name="phone">Phone; null to load all applicationUsers.</param>
        /// <param name="zipPostalCode">Phone; null to load all applicationUsers.</param>
        /// <param name="ipAddress">IP address; null to load all applicationUsers.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database.</param>
        /// <returns>ApplicationUsers.</returns>
        IPagedList<ApplicationUser> GetAllApplicationUsers(DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int affiliateId = 0, int vendorId = 0, int[] applicationUserRoleIds = null,
            string email = null, string username = null, string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null, string ipAddress = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        /// <summary>
        /// Gets online applicationUsers.
        /// </summary>
        /// <param name="lastActivityFromUtc">ApplicationUser last activity date (from).</param>
        /// <param name="applicationUserRoleIds">A list of applicationUser role identifiers to filter by (at least one match); pass null or empty list in order to load all applicationUsers; .</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>ApplicationUsers.</returns>
        IPagedList<ApplicationUser> GetOnlineApplicationUsers(DateTime lastActivityFromUtc,
            int[] applicationUserRoleIds, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Delete a applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        void DeleteApplicationUser(ApplicationUser applicationUser);

        /// <summary>
        /// Gets a applicationUser.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier.</param>
        /// <returns>A applicationUser.</returns>
        ApplicationUser GetApplicationUserById(int applicationUserId);

        /// <summary>
        /// Get applicationUsers by identifiers.
        /// </summary>
        /// <param name="applicationUserIds">ApplicationUser identifiers.</param>
        /// <returns>ApplicationUsers.</returns>
        IList<ApplicationUser> GetApplicationUsersByIds(int[] applicationUserIds);

        /// <summary>
        /// Gets a applicationUser by GUID.
        /// </summary>
        /// <param name="applicationUserGuid">ApplicationUser GUID.</param>
        /// <returns>A applicationUser.</returns>
        ApplicationUser GetApplicationUserByGuid(Guid applicationUserGuid);

        /// <summary>
        /// Get applicationUser by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>ApplicationUser.</returns>
        ApplicationUser GetApplicationUserByEmail(string email);

        /// <summary>
        /// Get applicationUser by system role.
        /// </summary>
        /// <param name="systemName">System name.</param>
        /// <returns>ApplicationUser.</returns>
        ApplicationUser GetApplicationUserBySystemName(string systemName);

        /// <summary>
        /// Get applicationUser by username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>ApplicationUser.</returns>
        ApplicationUser GetApplicationUserByUsername(string username);

        /// <summary>
        /// Insert a guest applicationUser.
        /// </summary>
        /// <returns>ApplicationUser.</returns>
        ApplicationUser InsertGuestApplicationUser();

        /// <summary>
        /// Insert a applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        void InsertApplicationUser(ApplicationUser applicationUser);

        /// <summary>
        /// Updates the applicationUser.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        void UpdateApplicationUser(ApplicationUser applicationUser);

        /// <summary>
        /// Reset data required for checkout.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="storeId">Store identifier.</param>
        /// <param name="clearCouponCodes">A value indicating whether to clear coupon code.</param>
        /// <param name="clearCheckoutAttributes">A value indicating whether to clear selected checkout attributes.</param>
        /// <param name="clearRewardPoints">A value indicating whether to clear "Use reward points" flag.</param>
        /// <param name="clearShippingMethod">A value indicating whether to clear selected shipping method.</param>
        /// <param name="clearPaymentMethod">A value indicating whether to clear selected payment method.</param>
        void ResetCheckoutData(ApplicationUser applicationUser, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true);

        /// <summary>
        /// Delete guest applicationUser records.
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records.</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records.</param>
        /// <param name="onlyWithoutShoppingCart">A value indicating whether to delete applicationUsers only without shopping cart.</param>
        /// <returns>Number of deleted applicationUsers.</returns>
        int DeleteGuestApplicationUsers(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart);

        /// <summary>
        /// Get full name.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>ApplicationUser full name.</returns>
        string GetApplicationUserFullName(ApplicationUser applicationUser);

        /// <summary>
        /// Formats the applicationUser name.
        /// </summary>
        /// <param name="applicationUser">Source.</param>
        /// <param name="stripTooLong">Strip too long applicationUser name.</param>
        /// <param name="maxLength">Maximum applicationUser name length.</param>
        /// <returns>Formatted text.</returns>
        string FormatUsername(ApplicationUser applicationUser, bool stripTooLong = false, int maxLength = 0);

        /// <summary>
        /// Gets coupon codes.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Coupon codes.</returns>
        string[] ParseAppliedDiscountCouponCodes(ApplicationUser applicationUser);

        /// <summary>
        /// Adds a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code.</param>
        void ApplyDiscountCouponCode(ApplicationUser applicationUser, string couponCode);

        /// <summary>
        /// Removes a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code to remove.</param>
        void RemoveDiscountCouponCode(ApplicationUser applicationUser, string couponCode);

        /// <summary>
        /// Gets coupon codes.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Coupon codes.</returns>
        string[] ParseAppliedGiftCardCouponCodes(ApplicationUser applicationUser);

        /// <summary>
        /// Adds a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code.</param>
        void ApplyGiftCardCouponCode(ApplicationUser applicationUser, string couponCode);

        /// <summary>
        /// Removes a coupon code.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="couponCode">Coupon code to remove.</param>
        void RemoveGiftCardCouponCode(ApplicationUser applicationUser, string couponCode);

        /// <summary>
        /// Delete a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        void DeleteApplicationUserRole(ApplicationUserRole applicationUserRole);

        /// <summary>
        /// Gets a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRoleId">ApplicationUser role identifier.</param>
        /// <returns>ApplicationUser role.</returns>
        ApplicationUserRole GetApplicationUserRoleById(int applicationUserRoleId);

        /// <summary>
        /// Gets a applicationUser role.
        /// </summary>
        /// <param name="systemName">ApplicationUser role system name.</param>
        /// <returns>ApplicationUser role.</returns>
        ApplicationUserRole GetApplicationUserRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all applicationUser roles.
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>ApplicationUser roles.</returns>
        IList<ApplicationUserRole> GetAllApplicationUserRoles(bool showHidden = false);

        /// <summary>
        /// Inserts a applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        void InsertApplicationUserRole(ApplicationUserRole applicationUserRole);

        /// <summary>
        /// Updates the applicationUser role.
        /// </summary>
        /// <param name="applicationUserRole">ApplicationUser role.</param>
        void UpdateApplicationUserRole(ApplicationUserRole applicationUserRole);

        /// <summary>
        /// Gets applicationUser passwords.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier; pass null to load all records.</param>
        /// <param name="passwordFormat">Password format; pass null to load all records.</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records.</param>
        /// <returns>List of applicationUser passwords.</returns>
        IList<ApplicationUserPassword> GetApplicationUserPasswords(int? applicationUserId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null);

        /// <summary>
        /// Get current applicationUser password.
        /// </summary>
        /// <param name="applicationUserId">ApplicationUser identifier.</param>
        /// <returns>ApplicationUser password.</returns>
        ApplicationUserPassword GetCurrentPassword(int applicationUserId);

        /// <summary>
        /// Insert a applicationUser password.
        /// </summary>
        /// <param name="applicationUserPassword">ApplicationUser password.</param>
        void InsertApplicationUserPassword(ApplicationUserPassword applicationUserPassword);

        /// <summary>
        /// Update a applicationUser password.
        /// </summary>
        /// <param name="applicationUserPassword">ApplicationUser password.</param>
        void UpdateApplicationUserPassword(ApplicationUserPassword applicationUserPassword);

        /// <summary>
        /// Check whether password recovery token is valid.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="token">Token to validate.</param>
        /// <returns>Result.</returns>
        bool IsPasswordRecoveryTokenValid(ApplicationUser applicationUser, string token);

        /// <summary>
        /// Check whether password recovery link is expired.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Result.</returns>
        bool IsPasswordRecoveryLinkExpired(ApplicationUser applicationUser);

        /// <summary>
        /// Check whether applicationUser password is expired.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>True if password is expired; otherwise false.</returns>
        bool PasswordIsExpired(ApplicationUser applicationUser);
    }
}
