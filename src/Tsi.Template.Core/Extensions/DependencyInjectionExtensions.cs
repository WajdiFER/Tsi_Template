using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Enums;

namespace Tsi.Template.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// This method will scan the whole application to find types with the injectable attribute
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            EngineContext.Current.GetAssemblies()
                .ConfigureByAssembly(assembly => services.ConfigureServices(configuration, assembly));

            EngineContext.Current.GetAssemblies()
                .ConfigureByAssembly(assembly => services.RegisterDependencies(assembly));

            EngineContext.Current.GetAssemblies()
                .ConfigureByAssembly(assembly => services.RegisterConsumers(assembly));
        }
         

        #region Utilities 
        private static void RegisterConsumers(this IServiceCollection services, Assembly assembly)
        {
            var consumers = assembly.FindClassesOfGenericType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
            {
                foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
                {
                    var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                    return isMatch;
                }, typeof(IConsumer<>)))
                {
                    services.AddScoped(findInterface, consumer);
                }
            }
        }

        private static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            assembly
                .FindClassesOfType<IInstaller>()
                .OrderBy(x => x.Order)
                .ToList()
                .ForEach(installer => installer.Install(services, configuration));
        }

        private static void RegisterDependencies(this IServiceCollection services, Assembly assembly)
        {
            foreach (Type currentType in assembly.FindClassesHavingAttribute<InjectableAttribute>())
            {
                services.RegisterDependency(currentType);
            }
        }

        private static void RegisterDependency(this IServiceCollection services, Type currentType)
        {
            var attributes = (InjectableAttribute[])currentType.GetCustomAttributes(typeof(InjectableAttribute), true);

            if(attributes.Length == 0)
            {
                return;
            }

            var attribute = attributes[0];
            var implementedInterface = attribute.ImplementedInterface;
            switch (attribute.Scope)
            {
                case DependencyInjectionScope.Scoped:
                    services.AddScoped(implementedInterface, currentType);
                    break;
                case DependencyInjectionScope.PerDependency:
                    services.AddTransient(implementedInterface, currentType);
                    break;
                case DependencyInjectionScope.Singleton:
                    services.AddSingleton(implementedInterface, currentType);
                    break;
            }
        }

        #endregion
    }
}
