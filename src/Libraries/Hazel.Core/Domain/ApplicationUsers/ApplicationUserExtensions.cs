using System;
using System.Linq;

namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser extensions.
    /// </summary>
    public static class ApplicationUserExtensions
    {
        /// <summary>
        /// Gets a value indicating whether applicationUser is in a certain applicationUser role.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="applicationUserRoleSystemName">ApplicationUser role system name.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsInApplicationUserRole(this ApplicationUser applicationUser,
            string applicationUserRoleSystemName, bool onlyActiveApplicationUserRoles = true)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (string.IsNullOrEmpty(applicationUserRoleSystemName))
                throw new ArgumentNullException(nameof(applicationUserRoleSystemName));

            var result = applicationUser.ApplicationUserRoles
                .FirstOrDefault(cr => (!onlyActiveApplicationUserRoles || cr.Active) && cr.SystemName == applicationUserRoleSystemName) != null;
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser a search engine.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Result.</returns>
        public static bool IsSearchEngineAccount(this ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (!applicationUser.IsSystemAccount || string.IsNullOrEmpty(applicationUser.SystemName))
                return false;

            var result = applicationUser.SystemName.Equals(HazelApplicationUserDefaults.SearchEngineApplicationUserName, StringComparison.InvariantCultureIgnoreCase);
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether the applicationUser is a built-in record for background tasks.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <returns>Result.</returns>
        public static bool IsBackgroundTaskAccount(this ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            if (!applicationUser.IsSystemAccount || string.IsNullOrEmpty(applicationUser.SystemName))
                return false;

            var result = applicationUser.SystemName.Equals(HazelApplicationUserDefaults.BackgroundTaskApplicationUserName, StringComparison.InvariantCultureIgnoreCase);
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser is administrator.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsAdmin(this ApplicationUser applicationUser, bool onlyActiveApplicationUserRoles = true)
        {
            return IsInApplicationUserRole(applicationUser, HazelApplicationUserDefaults.AdministratorsRoleName, onlyActiveApplicationUserRoles);
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser is a forum moderator.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsForumModerator(this ApplicationUser applicationUser, bool onlyActiveApplicationUserRoles = true)
        {
            return IsInApplicationUserRole(applicationUser, HazelApplicationUserDefaults.ForumModeratorsRoleName, onlyActiveApplicationUserRoles);
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser is registered.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsRegistered(this ApplicationUser applicationUser, bool onlyActiveApplicationUserRoles = true)
        {
            return IsInApplicationUserRole(applicationUser, HazelApplicationUserDefaults.RegisteredRoleName, onlyActiveApplicationUserRoles);
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser is guest.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsGuest(this ApplicationUser applicationUser, bool onlyActiveApplicationUserRoles = true)
        {
            return IsInApplicationUserRole(applicationUser, HazelApplicationUserDefaults.GuestsRoleName, onlyActiveApplicationUserRoles);
        }

        /// <summary>
        /// Gets a value indicating whether applicationUser is vendor.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="onlyActiveApplicationUserRoles">A value indicating whether we should look only in active applicationUser roles.</param>
        /// <returns>Result.</returns>
        public static bool IsVendor(this ApplicationUser applicationUser, bool onlyActiveApplicationUserRoles = true)
        {
            return IsInApplicationUserRole(applicationUser, HazelApplicationUserDefaults.VendorsRoleName, onlyActiveApplicationUserRoles);
        }

        /// <summary>
        /// Get applicationUser role identifiers.
        /// </summary>
        /// <param name="applicationUser">ApplicationUser.</param>
        /// <param name="showHidden">A value indicating whether to load hidden records.</param>
        /// <returns>ApplicationUser role identifiers.</returns>
        public static int[] GetApplicationUserRoleIds(this ApplicationUser applicationUser, bool showHidden = false)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var applicationUserRolesIds = applicationUser.ApplicationUserRoles
               .Where(cr => showHidden || cr.Active)
               .Select(cr => cr.Id)
               .ToArray();

            return applicationUserRolesIds;
        }
    }
}
