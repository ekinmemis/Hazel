using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// Messages settings.
    /// </summary>
    public class MessagesSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether UsePopupNotifications
        /// A value indicating whether popup notifications set as default.
        /// </summary>
        public bool UsePopupNotifications { get; set; }
    }
}
