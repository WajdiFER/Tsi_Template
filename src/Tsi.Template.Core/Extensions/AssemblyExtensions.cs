using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tsi.Template.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<T> FindClassesOfType<T>(this Assembly assembly)
        {
            return assembly
                .ExportedTypes
                .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        } 

        public static IEnumerable<Type> FindClassesOfGenericType<T>(this Assembly assembly)
        {
            return assembly.FindClassesOfGenericType(typeof(T));
        }

        public static IEnumerable<Type> FindClassesOfGenericType(this Assembly assembly, Type assignTypeFrom)
        {
            var result = new List<Type>();
            try
            {
                Type[] types = null;
                try
                {
                    types = assembly.GetTypes();
                }
                catch
                {
                }

                if (types == null)
                {
                    return null;
                }

                foreach (var t in types)
                {
                    if (!assignTypeFrom.IsAssignableFrom(t) && (!assignTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                        continue;

                    if (t.IsInterface)
                        continue;

                    if (t.IsClass && !t.IsAbstract)
                    {
                        result.Add(t);
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                {
                    msg += e.Message + Environment.NewLine;
                }

                var fail = new Exception(msg, ex);

                throw fail;
            }

            return result;
        }

        private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
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

        public static IEnumerable<Type> FindClassesHavingAttribute<TAttribute>(this Assembly assembly)
        {
            return assembly
                .ExportedTypes
                .Where(x => x.GetCustomAttributes(typeof(TAttribute), true).Length > 0);
        }

        public static void ConfigureByAssembly(this IEnumerable<Assembly> assemblies, Action<Assembly> action)
        {
            foreach (var assembly in assemblies)
            {
                action(assembly);
            }
        }

    }
}
