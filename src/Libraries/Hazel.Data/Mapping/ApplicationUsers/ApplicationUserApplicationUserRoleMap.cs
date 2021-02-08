using Hazel.Core.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser-applicationUser role mapping configuration.
    /// </summary>
    public partial class ApplicationUserApplicationUserRoleMap : HazelEntityTypeConfiguration<ApplicationUserApplicationUserRoleMapping>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<ApplicationUserApplicationUserRoleMapping> builder)
        {
            builder.ToTable(HazelMappingDefaults.ApplicationUserApplicationUserRoleTable);
            builder.HasKey(mapping => new { mapping.ApplicationUserId, mapping.ApplicationUserRoleId });

            builder.Property(mapping => mapping.ApplicationUserId).HasColumnName("ApplicationUser_Id");
            builder.Property(mapping => mapping.ApplicationUserRoleId).HasColumnName("ApplicationUserRole_Id");

            builder.HasOne(mapping => mapping.ApplicationUser)
                .WithMany(applicationUser => applicationUser.ApplicationUserApplicationUserRoleMappings)
                .HasForeignKey(mapping => mapping.ApplicationUserId)
                .IsRequired();

            builder.HasOne(mapping => mapping.ApplicationUserRole)
                .WithMany()
                .HasForeignKey(mapping => mapping.ApplicationUserRoleId)
                .IsRequired();

            builder.Ignore(mapping => mapping.Id);

            base.Configure(builder);
        }
    }
}
