using Hazel.Core.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser mapping configuration.
    /// </summary>
    public partial class ApplicationUserMap : HazelEntityTypeConfiguration<ApplicationUser>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser));
            builder.HasKey(applicationUser => applicationUser.Id);

            builder.Property(applicationUser => applicationUser.Username).HasMaxLength(1000);
            builder.Property(applicationUser => applicationUser.Email).HasMaxLength(1000);
            builder.Property(applicationUser => applicationUser.EmailToRevalidate).HasMaxLength(1000);
            builder.Property(applicationUser => applicationUser.SystemName).HasMaxLength(400);

            builder.Property(applicationUser => applicationUser.BillingAddressId).HasColumnName("BillingAddress_Id");
            builder.Property(applicationUser => applicationUser.ShippingAddressId).HasColumnName("ShippingAddress_Id");

            builder.Ignore(applicationUser => applicationUser.ApplicationUserRoles);


            base.Configure(builder);
        }
    }
}
