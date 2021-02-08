using Autofac;
using AutoMapper;
using Hazel.Core.Configuration;
using Hazel.Core.Infrastructure.DependencyManagement;
using Hazel.Core.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// Represents Hazel engine.
    /// </summary>
    public class HazelEngine : IEngine
    {
        /// <summary>
        /// Defines the _typeFinder.
        /// </summary>
        private ITypeFinder _typeFinder;

        /// <summary>
        /// Gets or sets the _serviceProvider
        /// Gets or sets service provider.
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        /// <summary>
        /// Get IServiceProvider.
        /// </summary>
        /// <returns>IServiceProvider.</returns>
        protected IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
                return null;
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// Run startup tasks.
        /// </summary>
        /// <param name="typeFinder">Type finder.</param>
        protected virtual void RunStartupTasks(ITypeFinder typeFinder)
        {
            //find startup tasks provided by other assemblies
            var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            //create and sort instances of startup tasks
            //we startup this interface even for not installed plugins. 
            //otherwise, DbContext initializers won't run and a plugin installation won't work
            var instances = startupTasks
                .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask.Order);

            //execute tasks
            foreach (var task in instances)
                task.Execute();
        }

        /// <summary>
        /// Register dependencies.
        /// </summary>
        /// <param name="containerBuilder">Container builder.</param>
        /// <param name="hazelConfig">Hazel configuration parameters.</param>
        public virtual void RegisterDependencies(ContainerBuilder containerBuilder, HazelConfig hazelConfig)
        {
            //register engine
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //register type finder
            containerBuilder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = _typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, _typeFinder, hazelConfig);
        }

        /// <summary>
        /// Register and configure AutoMapper.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        /// <param name="typeFinder">Type finder.</param>
        protected virtual void AddAutoMapper(IServiceCollection services, ITypeFinder typeFinder)
        {
            //find mapper configurations provided by other assemblies
            var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();

            //create and sort instances of mapper configurations
            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            //create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            //register
            AutoMapperConfiguration.Init(config);
        }

        /// <summary>
        /// The CurrentDomain_AssemblyResolve.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="args">The args<see cref="ResolveEventArgs"/>.</param>
        /// <returns>The <see cref="Assembly"/>.</returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            //get assembly from TypeFinder
            var tf = Resolve<ITypeFinder>();
            if (tf == null)
                return null;
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        /// <summary>
        /// Add and configure services.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        /// <param name="configuration">Configuration of the application.</param>
        /// <param name="hazelConfig">Hazel configuration parameters.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, HazelConfig hazelConfig)
        {
            //find startup configurations provided by other assemblies
            _typeFinder = new WebAppTypeFinder();
            var startupConfigurations = _typeFinder.FindClassesOfType<IHazelStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IHazelStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure services
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            //register mapper configurations
            AddAutoMapper(services, _typeFinder);

            //run startup tasks
            RunStartupTasks(_typeFinder);

            //resolve assemblies here. otherwise, plugins can throw an exception when rendering views
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// Configure HTTP request pipeline.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline.</param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            _serviceProvider = application.ApplicationServices;

            //find startup configurations provided by other assemblies
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<IHazelStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IHazelStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure request pipeline
            foreach (var instance in instances)
                instance.Configure(application);
        }

        /// <summary>
        /// Resolve dependency.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <returns>Resolved service.</returns>
        public TEntity Resolve<TEntity>() where TEntity : class
        {
            return (TEntity)Resolve(typeof(TEntity));
        }

        /// <summary>
        /// Resolve dependency.
        /// </summary>
        /// <param name="type">Type of resolved service.</param>
        /// <returns>Resolved service.</returns>
        public object Resolve(Type type)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;
            return sp.GetService(type);
        }

        /// <summary>
        /// Resolve dependencies.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <returns>Collection of resolved services.</returns>
        public virtual IEnumerable<TEntity> ResolveAll<TEntity>()
        {
            return (IEnumerable<TEntity>)GetServiceProvider().GetServices(typeof(TEntity));
        }

        /// <summary>
        /// Resolve unregistered service.
        /// </summary>
        /// <param name="type">Type of service.</param>
        /// <returns>Resolved service.</returns>
        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new HazelException("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new HazelException("No constructor was found that had all the dependencies satisfied.", innerException);
        }

        /// <summary>
        /// Gets the ServiceProvider
        /// Service provider.
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;
    }
}
