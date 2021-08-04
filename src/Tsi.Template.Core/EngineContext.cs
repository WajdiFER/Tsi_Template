using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Tsi.Template.Core
{
    public class EngineContext
    { 
        public static Context Current { get; private set; }

        public static void Create()
        {
            Current = new (); 
        } 
    }

    public class Context
    {
        private readonly HashSet<Assembly> ApplicationAssemblies = new();

        private IServiceProvider ServicesProvider { get; set; } 

        public void SetupServiceProvider(IServiceProvider serviceProvider)
        {
            ServicesProvider = serviceProvider;
        }

        public void LoadAssembly(Type type)
        {
            if (!ApplicationAssemblies.Contains(type.Assembly))
            {
                ApplicationAssemblies.Add(type.Assembly);
            } 
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            foreach (Assembly assembly in ApplicationAssemblies)
            {
                yield return assembly;
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        } 

        public object Resolve(Type type)
        {
            return GetServiceProvider().GetService(type);
        }
        public virtual IEnumerable<T> ResolveAll<T>()
        { 
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
        {
            if (scope == null)
            {
                var accessor = ServicesProvider?.GetService<IHttpContextAccessor>();
                var context = accessor?.HttpContext;
                return context?.RequestServices ?? ServicesProvider;
            }
            return scope.ServiceProvider;
        }

    }
}
