using Hazel.Core.Domain.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping.ApplicationUsers
{
    /// <summary>
    /// Represents a applicationUser password mapping configuration.
    /// </summary>
    public partial class ApplicationUserPasswordMap : HazelEntityTypeConfiguration<ApplicationUserPassword>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<ApplicationUserPassword> builder)
        {
            builder.ToTable(nameof(ApplicationUserPassword));
            builder.HasKey(password => password.Id);

            builder.HasOne(password => password.ApplicationUser)
                .WithMany()
                .HasForeignKey(password => password.ApplicationUserId)
                .IsRequired();

            builder.Ignore(password => password.PasswordFormat);

            base.Configure(builder);
        }
    }
}
