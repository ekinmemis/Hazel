using System.IO;

namespace Hazel.Services.Seo
{
    /// <summary>
    /// Represents a sitemap generator.
    /// </summary>
    public partial interface ISitemapGenerator
    {
        /// <summary>
        /// This will build an XML sitemap for better index with search engines.
        /// See http://en.wikipedia.org/wiki/Sitemaps for more information.
        /// </summary>
        /// <param name="id">Sitemap identifier.</param>
        /// <returns>Sitemap.xml as string.</returns>
        string Generate(int? id);

        /// <summary>
        /// This will build an XML sitemap for better index with search engines.
        /// See http://en.wikipedia.org/wiki/Sitemaps for more information.
        /// </summary>
        /// <param name="stream">Stream of sitemap.</param>
        /// <param name="id">Sitemap identifier.</param>
        void Generate(Stream stream, int? id);
    }
}
