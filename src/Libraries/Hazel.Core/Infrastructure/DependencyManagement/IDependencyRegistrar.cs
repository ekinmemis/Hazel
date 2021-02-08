using Autofac;
using Hazel.Core.Configuration;

namespace Hazel.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar interface.
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        /// <param name="typeFinder">Type finder.</param>
        /// <param name="config">Config.</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, HazelConfig config);

        /// <summary>
        /// Gets the Order
        /// Gets order of this dependency registrar implementation.
        /// </summary>
        int Order { get; }
    }
}
