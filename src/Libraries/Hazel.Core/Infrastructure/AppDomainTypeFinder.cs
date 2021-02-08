using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hazel.Core.Infrastructure
{
    /// <summary>
    /// A class that finds types needed by Hazel by looping assemblies in the 
    /// currently executing AppDomain. Only assemblies whose names matches
    /// certain patterns are investigated and an optional list of assemblies
    /// referenced by <see cref="AssemblyNames"/> are always investigated.
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        /// <summary>
        /// Defines the _ignoreReflectionErrors.
        /// </summary>
        private bool _ignoreReflectionErrors = true;

        /// <summary>
        /// Defines the _fileProvider.
        /// </summary>
        protected IHazelFileProvider _fileProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDomainTypeFinder"/> class.
        /// </summary>
        /// <param name="fileProvider">The fileProvider<see cref="IHazelFileProvider"/>.</param>
        public AppDomainTypeFinder(IHazelFileProvider fileProvider = null)
        {
            _fileProvider = fileProvider ?? CommonHelper.DefaultFileProvider;
        }

        /// <summary>
        /// Iterates all assemblies in the AppDomain and if it's name matches the configured patterns add it to our list.
        /// </summary>
        /// <param name="addedAssemblyNames">.</param>
        /// <param name="assemblies">.</param>
        private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!Matches(assembly.FullName))
                    continue;

                if (addedAssemblyNames.Contains(assembly.FullName))
                    continue;

                assemblies.Add(assembly);
                addedAssemblyNames.Add(assembly.FullName);
            }
        }

        /// <summary>
        /// Adds specifically configured assemblies.
        /// </summary>
        /// <param name="addedAssemblyNames">.</param>
        /// <param name="assemblies">.</param>
        protected virtual void AddConfiguredAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            foreach (var assemblyName in AssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                if (addedAssemblyNames.Contains(assembly.FullName))
                    continue;

                assemblies.Add(assembly);
                addedAssemblyNames.Add(assembly.FullName);
            }
        }

        /// <summary>
        /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">The assemblyFullName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, AssemblySkipLoadingPattern)
                   && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);
        }

        /// <summary>
        /// Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">The assemblyFullName<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// Makes sure matching assemblies in the supplied folder are loaded in the app domain.
        /// </summary>
        /// <param name="directoryPath">The directoryPath<see cref="string"/>.</param>
        protected virtual void LoadMatchingAssemblies(string directoryPath)
        {
            var loadedAssemblyNames = new List<string>();

            foreach (var a in GetAssemblies())
            {
                loadedAssemblyNames.Add(a.FullName);
            }

            if (!_fileProvider.DirectoryExists(directoryPath))
            {
                return;
            }

            foreach (var dllPath in _fileProvider.GetFiles(directoryPath, "*.dll"))
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllPath);
                    if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                    {
                        App.Load(an);
                    }

                    //old loading stuff
                    //Assembly a = Assembly.ReflectionOnlyLoadFrom(dllPath);
                    //if (Matches(a.FullName) && !loadedAssemblyNames.Contains(a.FullName))
                    //{
                    //    App.Load(a.FullName);
                    //}
                }
                catch (BadImageFormatException ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Does type implement generic?.
        /// </summary>
        /// <param name="type">.</param>
        /// <param name="openGeneric">.</param>
        /// <returns>.</returns>
        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    if (genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find classes of type.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes.</param>
        /// <returns>Result.</returns>
        public IEnumerable<Type> FindClassesOfType<TEntity>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(TEntity), onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type.
        /// </summary>
        /// <param name="assignTypeFrom">Assign type from.</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes.</param>
        /// <returns>Result.</returns>
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="assemblies">Assemblies.</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes.</param>
        /// <returns>Result.</returns>
        public IEnumerable<Type> FindClassesOfType<TEntity>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(TEntity), assemblies, onlyConcreteClasses);
        }

        /// <summary>
        /// Find classes of type.
        /// </summary>
        /// <param name="assignTypeFrom">Assign type from.</param>
        /// <param name="assemblies">Assemblies.</param>
        /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes.</param>
        /// <returns>Result.</returns>
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!_ignoreReflectionErrors)
                        {
                            throw;
                        }
                    }

                    if (types == null)
                        continue;

                    foreach (var t in types)
                    {
                        if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            continue;

                        if (t.IsInterface)
                            continue;

                        if (onlyConcreteClasses)
                        {
                            if (t.IsClass && !t.IsAbstract)
                            {
                                result.Add(t);
                            }
                        }
                        else
                        {
                            result.Add(t);
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                Debug.WriteLine(fail.Message, fail);

                throw fail;
            }

            return result;
        }

        /// <summary>
        /// Gets the assemblies related to the current implementation.
        /// </summary>
        /// <returns>A list of assemblies.</returns>
        public virtual IList<Assembly> GetAssemblies()
        {
            var addedAssemblyNames = new List<string>();
            var assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
                AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
            AddConfiguredAssemblies(addedAssemblyNames, assemblies);

            return assemblies;
        }

        /// <summary>
        /// Gets the App.
        /// </summary>
        public virtual AppDomain App => AppDomain.CurrentDomain;

        /// <summary>
        /// Gets or sets a value indicating whether LoadAppDomainAssemblies.
        /// </summary>
        public bool LoadAppDomainAssemblies { get; set; } = true;

        /// <summary>
        /// Gets or sets the AssemblyNames.
        /// </summary>
        public IList<string> AssemblyNames { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the AssemblySkipLoadingPattern.
        /// </summary>
        public string AssemblySkipLoadingPattern { get; set; } = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

        /// <summary>
        /// Gets or sets the AssemblyRestrictToLoadingPattern.
        /// </summary>
        public string AssemblyRestrictToLoadingPattern { get; set; } = ".*";
    }
}
