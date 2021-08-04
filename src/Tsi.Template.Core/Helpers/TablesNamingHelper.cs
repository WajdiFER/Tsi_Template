using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Template.Core.Helpers
{  
    public static class TablesNamingHelper
    {
        private const string Prefix = "";

        public static string NameOf<T>() => NameOf(typeof(T));

        public static string NameOf(Type tableType) => tableType.Name.EndsWith("s") ? $"{Prefix}{tableType.Name}" : $"{Prefix}{tableType.Name}s";
    }
}
