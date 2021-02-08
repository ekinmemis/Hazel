using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Directory;
using Hazel.Core.Domain.Localization;

namespace Hazel.Core
{
    /// <summary>
    /// Represents work context.
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current applicationUser.
        /// </summary>
        ApplicationUser CurrentApplicationUser { get; set; }

        /// <summary>
        /// Gets the original applicationUser (in case the current one is impersonated).
        /// </summary>
        ApplicationUser OriginalApplicationUserIfImpersonated { get; }

        /// <summary>
        /// Gets or sets the WorkingLanguage
        /// Gets or sets current user working language.
        /// </summary>
        Language WorkingLanguage { get; set; }

        /// <summary>
        /// Gets or sets the WorkingCurrency
        /// Gets or sets current user working currency.
        /// </summary>
        Currency WorkingCurrency { get; set; }

        /// <summary>
        /// Gets or sets the IsAdmin
        /// Gets or sets value indicating whether we're in admin area.
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
