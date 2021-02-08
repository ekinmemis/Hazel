using Autofac;
using Hazel.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the various services composing the Hazel engine. 
    /// Edit functionality, modules and implementations access most Hazel functionality through this interface.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Add and configure services.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        /// <param name="configuration">Configuration of the application.</param>
        /// <param name="hazelConfig">Hazel configuration parameters.</param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, HazelConfig hazelConfig);

        /// <summary>
        /// Configure HTTP request pipeline.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline.</param>
        void ConfigureRequestPipeline(IApplicationBuilder application);

        /// <summary>
        /// Resolve dependency.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <returns>Resolved service.</returns>
        TEntity Resolve<TEntity>() where TEntity : class;

        /// <summary>
        /// Resolve dependency.
        /// </summary>
        /// <param name="type">Type of resolved service.</param>
        /// <returns>Resolved service.</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve dependencies.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <returns>Collection of resolved services.</returns>
        IEnumerable<TEntity> ResolveAll<TEntity>();

        /// <summary>
        /// Resolve unregistered service.
        /// </summary>
        /// <param name="type">Type of service.</param>
        /// <returns>Resolved service.</returns>
        object ResolveUnregistered(Type type);

        /// <summary>
        /// Register dependencies.
        /// </summary>
        /// <param name="containerBuilder">Container builder.</param>
        /// <param name="hazelConfig">Hazel configuration parameters.</param>
        void RegisterDependencies(ContainerBuilder containerBuilder, HazelConfig hazelConfig);
    }
}
