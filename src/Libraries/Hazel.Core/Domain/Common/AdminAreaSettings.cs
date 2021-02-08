using Hazel.Core.Configuration;

namespace Hazel.Core.Domain.Common
{
    /// <summary>
    /// Admin area settings.
    /// </summary>
    public class AdminAreaSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the DefaultGridPageSize
        /// Default grid page size.
        /// </summary>
        public int DefaultGridPageSize { get; set; }

        /// <summary>
        /// Gets or sets the PopupGridPageSize
        /// Popup grid page size (for popup pages).
        /// </summary>
        public int PopupGridPageSize { get; set; }

        /// <summary>
        /// Gets or sets the GridPageSizes
        /// A comma-separated list of available grid page sizes.
        /// </summary>
        public string GridPageSizes { get; set; }

        /// <summary>
        /// Gets or sets the RichEditorAdditionalSettings
        /// Additional settings for rich editor.
        /// </summary>
        public string RichEditorAdditionalSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RichEditorAllowJavaScript
        /// A value indicating whether to javascript is supported in rich editor.
        /// </summary>
        public bool RichEditorAllowJavaScript { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RichEditorAllowStyleTag
        /// A value indicating whether to style tag is supported in rich editor.
        /// </summary>
        public bool RichEditorAllowStyleTag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseRichEditorForApplicationUserEmails
        /// A value indicating whether to use rich text editor on email messages for applicationUsers.
        /// </summary>
        public bool UseRichEditorForApplicationUserEmails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseRichEditorInMessageTemplates
        /// A value indicating whether to use rich editor on message templates and campaigns details pages.
        /// </summary>
        public bool UseRichEditorInMessageTemplates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether advertisements (news) should be hidden.
        /// </summary>
        public bool HideAdvertisementsOnAdminArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should display a recommendation about copyright removal key.
        /// </summary>
        public bool CheckCopyrightRemovalKey { get; set; }

        /// <summary>
        /// Gets or sets the LastNewsTitleAdminArea
        /// Gets or sets title of last news (admin area).
        /// </summary>
        public string LastNewsTitleAdminArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use IsoDateFormat in JSON results (used for avoiding issue with dates in grids).
        /// </summary>
        public bool UseIsoDateFormatInJsonResult { get; set; }
    }
}
