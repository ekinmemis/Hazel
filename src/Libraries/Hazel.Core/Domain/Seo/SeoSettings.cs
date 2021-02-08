using Hazel.Core.Configuration;
using System.Collections.Generic;

namespace Hazel.Core.Domain.Seo
{
    /// <summary>
    /// SEO settings.
    /// </summary>
    public class SeoSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the PageTitleSeparator
        /// Page title separator.
        /// </summary>
        public string PageTitleSeparator { get; set; }

        /// <summary>
        /// Gets or sets the PageTitleSeoAdjustment
        /// Page title SEO adjustment.
        /// </summary>
        public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }

        /// <summary>
        /// Gets or sets the DefaultTitle
        /// Default title.
        /// </summary>
        public string DefaultTitle { get; set; }

        /// <summary>
        /// Gets or sets the DefaultMetaKeywords
        /// Default META keywords.
        /// </summary>
        public string DefaultMetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the DefaultMetaDescription
        /// Default META description.
        /// </summary>
        public string DefaultMetaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether GenerateProductMetaDescription
        /// A value indicating whether product META descriptions will be generated automatically (if not entered).
        /// </summary>
        public bool GenerateProductMetaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ConvertNonWesternChars
        /// A value indicating whether we should convert non-western chars to western ones.
        /// </summary>
        public bool ConvertNonWesternChars { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AllowUnicodeCharsInUrls
        /// A value indicating whether unicode chars are allowed.
        /// </summary>
        public bool AllowUnicodeCharsInUrls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanonicalUrlsEnabled
        /// A value indicating whether canonical URL tags should be used.
        /// </summary>
        public bool CanonicalUrlsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether QueryStringInCanonicalUrlsEnabled
        /// A value indicating whether to use canonical URLs with query string parameters.
        /// </summary>
        public bool QueryStringInCanonicalUrlsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the WwwRequirement
        /// WWW requires (with or without WWW).
        /// </summary>
        public WwwRequirement WwwRequirement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether TwitterMetaTags
        /// A value indicating whether Twitter META tags should be generated.
        /// </summary>
        public bool TwitterMetaTags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OpenGraphMetaTags
        /// A value indicating whether Open Graph META tags should be generated.
        /// </summary>
        public bool OpenGraphMetaTags { get; set; }

        /// <summary>
        /// Gets or sets the ReservedUrlRecordSlugs
        /// Slugs (sename) reserved for some other needs.
        /// </summary>
        public List<string> ReservedUrlRecordSlugs { get; set; }

        /// <summary>
        /// Gets or sets the CustomHeadTags
        /// Custom tags in the <![CDATA[<head></head>]]> section.
        /// </summary>
        public string CustomHeadTags { get; set; }
    }
}
