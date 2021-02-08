namespace Hazel.Core.Domain.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser-applicationUser role mapping class.
    /// </summary>
    public partial class ApplicationUserApplicationUserRoleMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the applicationUser identifier.
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role identifier.
        /// </summary>
        public int ApplicationUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationUser.
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser role.
        /// </summary>
        public virtual ApplicationUserRole ApplicationUserRole { get; set; }
    }
}
