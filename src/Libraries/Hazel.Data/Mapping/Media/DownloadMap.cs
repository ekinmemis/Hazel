using Hazel.Core.Domain.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.Media
{
    /// <summary>
    /// Represents a download mapping configuration.
    /// </summary>
    public partial class DownloadMap : HazelEntityTypeConfiguration<Download>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<Download> builder)
        {
            builder.ToTable(nameof(Download));
            builder.HasKey(download => download.Id);

            base.Configure(builder);
        }
    }
}
