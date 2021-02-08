using Hazel.Core.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser role mapping configuration.
    /// </summary>
    public partial class ApplicationUserRoleMap : HazelEntityTypeConfiguration<ApplicationUserRole>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable(nameof(ApplicationUserRole));
            builder.HasKey(role => role.Id);

            builder.Property(role => role.Name).HasMaxLength(255).IsRequired();
            builder.Property(role => role.SystemName).HasMaxLength(255);

            base.Configure(builder);
        }
    }
}
