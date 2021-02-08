using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// ApplicationUser registration result.
    /// </summary>
    public class ApplicationUserRegistrationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRegistrationResult"/> class.
        /// </summary>
        public ApplicationUserRegistrationResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully.
        /// </summary>
        public bool Success => !Errors.Any();

        /// <summary>
        /// Add error.
        /// </summary>
        /// <param name="error">Error.</param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Gets or sets the Errors
        /// Errors.
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
