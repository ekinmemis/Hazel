using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hazel.Data.Mapping
{
    /// <summary>
    /// Represents base query type mapping configuration.
    /// </summary>
    /// <typeparam name="TQuery">Query type type.</typeparam>
    [System.Obsolete]
    public partial class HazelQueryTypeConfiguration<TQuery> : IMappingConfiguration, IQueryTypeConfiguration<TQuery> where TQuery : class
    {
        /// <summary>
        /// Developers can override this method in custom partial classes in order to add some custom configuration code.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the query.</param>
        protected virtual void PostConfigure(QueryTypeBuilder<TQuery> builder)
        {
        }

        /// <summary>
        /// Configures the query type.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the query type.</param>
        public virtual void Configure(QueryTypeBuilder<TQuery> builder)
        {
            //add custom configuration
            PostConfigure(builder);
        }

        /// <summary>
        /// Apply this mapping configuration.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the database context.</param>
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
