using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.ApplicationUsers
{
    /// <summary>
    /// Change password result.
    /// </summary>
    public class ChangePasswordResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordResult"/> class.
        /// </summary>
        public ChangePasswordResult()
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
