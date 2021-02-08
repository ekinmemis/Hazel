using Hazel.Core;
using Hazel.Core.Domain.Common;
using Hazel.Core.Domain.Localization;
using Hazel.Core.Domain.Security;
using Hazel.Core.Domain.Seo;
using Hazel.Services.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Hazel.Services.Seo
{
    /// <summary>
    /// Represents a sitemap generator.
    /// </summary>
    public partial class SitemapGenerator : ISitemapGenerator
    {
        /// <summary>
        /// Defines the _actionContextAccessor.
        /// </summary>
        private readonly IActionContextAccessor _actionContextAccessor;

        /// <summary>
        /// Defines the _languageService.
        /// </summary>
        private readonly ILanguageService _languageService;

        /// <summary>
        /// Defines the _urlHelperFactory.
        /// </summary>
        private readonly IUrlHelperFactory _urlHelperFactory;

        /// <summary>
        /// Defines the _urlRecordService.
        /// </summary>
        private readonly IUrlRecordService _urlRecordService;

        /// <summary>
        /// Defines the _webHelper.
        /// </summary>
        private readonly IWebHelper _webHelper;

        /// <summary>
        /// Defines the _localizationSettings.
        /// </summary>
        private readonly LocalizationSettings _localizationSettings;

        /// <summary>
        /// Defines the _securitySettings.
        /// </summary>
        private readonly SecuritySettings _securitySettings;

        /// <summary>
        /// Defines the _sitemapXmlSettings.
        /// </summary>
        private readonly SitemapXmlSettings _sitemapXmlSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapGenerator"/> class.
        /// </summary>
        /// <param name="actionContextAccessor">The actionContextAccessor<see cref="IActionContextAccessor"/>.</param>
        /// <param name="languageService">The languageService<see cref="ILanguageService"/>.</param>
        /// <param name="urlHelperFactory">The urlHelperFactory<see cref="IUrlHelperFactory"/>.</param>
        /// <param name="urlRecordService">The urlRecordService<see cref="IUrlRecordService"/>.</param>
        /// <param name="webHelper">The webHelper<see cref="IWebHelper"/>.</param>
        /// <param name="localizationSettings">The localizationSettings<see cref="LocalizationSettings"/>.</param>
        /// <param name="securitySettings">The securitySettings<see cref="SecuritySettings"/>.</param>
        /// <param name="sitemapSettings">The sitemapSettings<see cref="SitemapXmlSettings"/>.</param>
        public SitemapGenerator(
            IActionContextAccessor actionContextAccessor,
            ILanguageService languageService,
            IUrlHelperFactory urlHelperFactory,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            LocalizationSettings localizationSettings,
            SecuritySettings securitySettings,
            SitemapXmlSettings sitemapSettings)
        {
            _actionContextAccessor = actionContextAccessor;
            _languageService = languageService;
            _urlHelperFactory = urlHelperFactory;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _localizationSettings = localizationSettings;
            _securitySettings = securitySettings;
            _sitemapXmlSettings = sitemapSettings;
        }

        /// <summary>
        /// Represents sitemap URL entry.
        /// </summary>
        protected class SitemapUrl
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SitemapUrl"/> class.
            /// </summary>
            /// <param name="location">URL of the page.</param>
            /// <param name="alternateLocations">List of the page urls.</param>
            /// <param name="frequency">Update frequency.</param>
            /// <param name="updatedOn">Updated on.</param>
            public SitemapUrl(string location, IList<string> alternateLocations, UpdateFrequency frequency, DateTime updatedOn)
            {
                Location = location;
                AlternateLocations = alternateLocations;
                UpdateFrequency = frequency;
                UpdatedOn = updatedOn;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SitemapUrl"/> class.
            /// </summary>
            /// <param name="location">URL of the page.</param>
            /// <param name="anotheUrl">The another site map url.</param>
            public SitemapUrl(string location, SitemapUrl anotheUrl)
            {
                Location = location;
                AlternateLocations = anotheUrl.AlternateLocations;
                UpdateFrequency = anotheUrl.UpdateFrequency;
                UpdatedOn = anotheUrl.UpdatedOn;
            }

            /// <summary>
            /// Gets or sets the Location
            /// Gets or sets URL of the page.
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// Gets or sets the AlternateLocations
            /// Gets or sets localized URLs of the page.
            /// </summary>
            public IList<string> AlternateLocations { get; set; }

            /// <summary>
            /// Gets or sets the UpdateFrequency
            /// Gets or sets a value indicating how frequently the page is likely to change.
            /// </summary>
            public UpdateFrequency UpdateFrequency { get; set; }

            /// <summary>
            /// Gets or sets the date of last modification of the file.
            /// </summary>
            public DateTime UpdatedOn { get; set; }
        }

        /// <summary>
        /// Get UrlHelper.
        /// </summary>
        /// <returns>UrlHelper.</returns>
        protected virtual IUrlHelper GetUrlHelper()
        {
            return _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
        }

        /// <summary>
        /// Get HTTP protocol.
        /// </summary>
        /// <returns>Protocol name as string.</returns>
        protected virtual string GetHttpProtocol()
        {
            return _securitySettings.ForceSslForAllPages ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
        }

        /// <summary>
        /// Generate URLs for the sitemap.
        /// </summary>
        /// <returns>List of sitemap URLs.</returns>
        protected virtual IList<SitemapUrl> GenerateUrls()
        {
            var sitemapUrls = new List<SitemapUrl>
            {
                //home page
                GetLocalizedSitemapUrl("Homepage"),

                //search products
                GetLocalizedSitemapUrl("ProductSearch"),

                //contact us
                GetLocalizedSitemapUrl("ContactUs")
            };

            //custom URLs
            if (_sitemapXmlSettings.SitemapXmlIncludeCustomUrls)
                sitemapUrls.AddRange(GetCustomUrls());

            return sitemapUrls;
        }

        /// <summary>
        /// Get custom URLs for the sitemap.
        /// </summary>
        /// <returns>Sitemap URLs.</returns>
        protected virtual IEnumerable<SitemapUrl> GetCustomUrls()
        {
            var storeLocation = _webHelper.GetStoreLocation();

            return _sitemapXmlSettings.SitemapCustomUrls.Select(customUrl =>
                new SitemapUrl(string.Concat(storeLocation, customUrl), new List<string>(), UpdateFrequency.Weekly, DateTime.UtcNow));
        }

        /// <summary>
        /// Get route params for URL localization.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="model">Model.</param>
        /// <returns>Lambda for route params.</returns>
        protected virtual Func<int?, object> GetSeoRouteParams<TEntity>(TEntity model)
            where TEntity : BaseEntity, ISlugSupported
        {
            return lang => new { SeName = _urlRecordService.GetSeName(model, lang) };
        }

        /// <summary>
        /// Return localized urls.
        /// </summary>
        /// <param name="routeName">Route name.</param>
        /// <param name="routeParams">Lambda for route params object.</param>
        /// <param name="dateTimeUpdatedOn">A time when URL was updated last time.</param>
        /// <param name="updateFreq">How often to update url.</param>
        /// <returns>The <see cref="SitemapUrl"/>.</returns>
        protected virtual SitemapUrl GetLocalizedSitemapUrl(string routeName,
            Func<int?, object> routeParams = null,
            DateTime? dateTimeUpdatedOn = null,
            UpdateFrequency updateFreq = UpdateFrequency.Weekly)
        {
            var urlHelper = GetUrlHelper();

            //url for current language
            var url = urlHelper.RouteUrl(routeName, routeParams?.Invoke(null), GetHttpProtocol());

            var updatedOn = dateTimeUpdatedOn ?? DateTime.UtcNow;
            var languages = _localizationSettings.SeoFriendlyUrlsForLanguagesEnabled
                ? _languageService.GetAllLanguages()
                : null;

            if (languages == null)
                return new SitemapUrl(url, new List<string>(), updateFreq, updatedOn);

            var pathBase = _actionContextAccessor.ActionContext.HttpContext.Request.PathBase;
            //return list of localized urls
            var localizedUrls = languages
                .Select(lang =>
                {
                    var currentUrl = urlHelper.RouteUrl(routeName, routeParams?.Invoke(lang.Id), GetHttpProtocol());

                    if (string.IsNullOrEmpty(currentUrl))
                        return null;

                    //Extract server and path from url
                    var scheme = new Uri(currentUrl).GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    var path = new Uri(currentUrl).PathAndQuery;

                    //Replace seo code
                    var localizedPath = path
                        .RemoveLanguageSeoCodeFromUrl(pathBase, true)
                        .AddLanguageSeoCodeToUrl(pathBase, true, lang);

                    return new Uri(new Uri(scheme), localizedPath).ToString();
                })
                .Where(value => !string.IsNullOrEmpty(value))
                .ToList();

            return new SitemapUrl(url, localizedUrls, updateFreq, updatedOn);
        }

        /// <summary>
        /// Write sitemap index file into the stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="sitemapNumber">The number of sitemaps.</param>
        protected virtual void WriteSitemapIndex(Stream stream, int sitemapNumber)
        {
            var urlHelper = GetUrlHelper();

            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("sitemapindex");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
                writer.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                //write URLs of all available sitemaps
                for (var id = 1; id <= sitemapNumber; id++)
                {
                    var url = urlHelper.RouteUrl("sitemap-indexed.xml", new { Id = id }, GetHttpProtocol());
                    var location = XmlHelper.XmlEncode(url);

                    writer.WriteStartElement("sitemap");
                    writer.WriteElementString("loc", location);
                    writer.WriteElementString("lastmod", DateTime.UtcNow.ToString(HazelSeoDefaults.SitemapDateFormat));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Write sitemap file into the stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="sitemapUrls">List of sitemap URLs.</param>
        protected virtual void WriteSitemap(Stream stream, IList<SitemapUrl> sitemapUrls)
        {
            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
                writer.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                //write URLs from list to the sitemap
                foreach (var sitemapUrl in sitemapUrls)
                {
                    //write base url
                    WriteSitemapUrl(writer, sitemapUrl);

                    //write all alternate url if exists
                    foreach (var alternate in sitemapUrl.AlternateLocations
                        .Where(p => !p.Equals(sitemapUrl.Location, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        WriteSitemapUrl(writer, new SitemapUrl(alternate, sitemapUrl));
                    }
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Write sitemap.
        /// </summary>
        /// <param name="writer">XML stream writer.</param>
        /// <param name="sitemapUrl">Sitemap URL.</param>
        protected virtual void WriteSitemapUrl(XmlTextWriter writer, SitemapUrl sitemapUrl)
        {
            if (string.IsNullOrEmpty(sitemapUrl.Location))
                return;

            writer.WriteStartElement("url");

            var loc = XmlHelper.XmlEncode(sitemapUrl.Location);
            writer.WriteElementString("loc", loc);

            //write all related url
            foreach (var alternate in sitemapUrl.AlternateLocations)
            {
                if (string.IsNullOrEmpty(alternate))
                    continue;

                //extract seo code
                var altLoc = XmlHelper.XmlEncode(alternate);
                var altLocPath = new Uri(altLoc).PathAndQuery;
                altLocPath.IsLocalizedUrl(_actionContextAccessor.ActionContext.HttpContext.Request.PathBase, true, out var lang);

                if (string.IsNullOrEmpty(lang?.UniqueSeoCode))
                    continue;

                writer.WriteStartElement("xhtml:link");
                writer.WriteAttributeString("rel", "alternate");
                writer.WriteAttributeString("hreflang", lang.UniqueSeoCode);
                writer.WriteAttributeString("href", altLoc);
                writer.WriteEndElement();
            }

            writer.WriteElementString("changefreq", sitemapUrl.UpdateFrequency.ToString().ToLowerInvariant());
            writer.WriteElementString("lastmod", sitemapUrl.UpdatedOn.ToString(HazelSeoDefaults.SitemapDateFormat, CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }

        /// <summary>
        /// This will build an XML sitemap for better index with search engines.
        /// See http://en.wikipedia.org/wiki/Sitemaps for more information.
        /// </summary>
        /// <param name="id">Sitemap identifier.</param>
        /// <returns>Sitemap.xml as string.</returns>
        public virtual string Generate(int? id)
        {
            using (var stream = new MemoryStream())
            {
                Generate(stream, id);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// This will build an XML sitemap for better index with search engines.
        /// See http://en.wikipedia.org/wiki/Sitemaps for more information.
        /// </summary>
        /// <param name="stream">Stream of sitemap.</param>
        /// <param name="id">Sitemap identifier.</param>
        public virtual void Generate(Stream stream, int? id)
        {
            //generate all URLs for the sitemap
            var sitemapUrls = GenerateUrls();

            //split URLs into separate lists based on the max size 
            var sitemaps = sitemapUrls
                .Select((url, index) => new { Index = index, Value = url })
                .GroupBy(group => group.Index / HazelSeoDefaults.SitemapMaxUrlNumber)
                .Select(group => group
                    .Select(url => url.Value)
                    .ToList()).ToList();

            if (!sitemaps.Any())
                return;

            if (id.HasValue)
            {
                //requested sitemap does not exist
                if (id.Value == 0 || id.Value > sitemaps.Count)
                    return;

                //otherwise write a certain numbered sitemap file into the stream
                WriteSitemap(stream, sitemaps.ElementAt(id.Value - 1));
            }
            else
            {
                //URLs more than the maximum allowable, so generate a sitemap index file
                if (sitemapUrls.Count >= HazelSeoDefaults.SitemapMaxUrlNumber)
                {
                    //write a sitemap index file into the stream
                    WriteSitemapIndex(stream, sitemaps.Count);
                }
                else
                {
                    //otherwise generate a standard sitemap
                    WriteSitemap(stream, sitemaps.First());
                }
            }
        }
    }
}
