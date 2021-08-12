using System;
using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Core.Helpers
{
    public static class TablesNamingHelper
    { 
        public static string NameOf<T>() => NameOf(typeof(T));

        public static string NameOf(Type tableType)
        {
            string prefix;

            if (typeof(ICommonEntity).IsAssignableFrom(tableType))
            {
                prefix = "Common_";
            }
            else
            {
                prefix = "App_";
            }

            return tableType.Name.EndsWith("s") ? $"{prefix}{tableType.Name}" : $"{prefix}{tableType.Name}s";
        }
    }
}
