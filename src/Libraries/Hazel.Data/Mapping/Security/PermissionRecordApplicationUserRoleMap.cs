using Hazel.Core.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.Security
{
    /// <summary>
    /// Represents a permission record-applicationUser role mapping configuration.
    /// </summary>
    public partial class PermissionRecordApplicationUserRoleMap : HazelEntityTypeConfiguration<PermissionRecordApplicationUserRoleMapping>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<PermissionRecordApplicationUserRoleMapping> builder)
        {
            builder.ToTable(HazelMappingDefaults.PermissionRecordRoleTable);
            builder.HasKey(mapping => new { mapping.PermissionRecordId, mapping.ApplicationUserRoleId });

            builder.Property(mapping => mapping.PermissionRecordId).HasColumnName("PermissionRecord_Id");
            builder.Property(mapping => mapping.ApplicationUserRoleId).HasColumnName("ApplicationUserRole_Id");

            builder.HasOne(mapping => mapping.ApplicationUserRole)
                .WithMany(role => role.PermissionRecordApplicationUserRoleMappings)
                .HasForeignKey(mapping => mapping.ApplicationUserRoleId)
                .IsRequired();

            builder.HasOne(mapping => mapping.PermissionRecord)
                .WithMany(record => record.PermissionRecordApplicationUserRoleMappings)
                .HasForeignKey(mapping => mapping.PermissionRecordId)
                .IsRequired();

            builder.Ignore(mapping => mapping.Id);

            base.Configure(builder);
        }
    }
}
