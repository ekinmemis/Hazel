using Hazel.Core.Domain.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.Logging
{
    /// <summary>
    /// Represents an activity log mapping configuration.
    /// </summary>
    public partial class ActivityLogMap : HazelEntityTypeConfiguration<ActivityLog>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable(nameof(ActivityLog));
            builder.HasKey(logItem => logItem.Id);

            builder.Property(logItem => logItem.Comment).IsRequired();
            builder.Property(logItem => logItem.IpAddress).HasMaxLength(200);
            builder.Property(logItem => logItem.EntityName).HasMaxLength(400);

            builder.HasOne(logItem => logItem.ActivityLogType)
                .WithMany()
                .HasForeignKey(logItem => logItem.ActivityLogTypeId)
                .IsRequired();

            builder.HasOne(logItem => logItem.ApplicationUser)
                .WithMany()
                .HasForeignKey(logItem => logItem.ApplicationUserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
