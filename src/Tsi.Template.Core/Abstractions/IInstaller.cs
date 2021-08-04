using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tsi.Template.Core.Abstractions
{
    /// <summary>
    /// This Interface is used to find all the types that has dependency injection logic and execute the Install method in them
    /// Be Aware that no dependency can be injected in any of these classes
    /// </summary>
    public interface IInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);

        int Order { get; }
    }
}
