using System;
using Tsi.Template.Core.Enums;

namespace Tsi.Template.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class InjectableAttribute : Attribute
    {
        public Type ImplementedInterface { get; }
        public DependencyInjectionScope Scope { get; }

        public InjectableAttribute(Type implementedInterface, DependencyInjectionScope scope = DependencyInjectionScope.Scoped)
        {
            ImplementedInterface = implementedInterface;
            Scope = scope;
        }
    }
}
