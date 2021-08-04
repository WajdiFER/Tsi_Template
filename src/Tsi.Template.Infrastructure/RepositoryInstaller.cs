using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Infrastructure.Data;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Infrastructure
{
    public class RepositoryInstaller : IInstaller
    {
        public int Order => 1;

        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IDatabaseFactory), typeof(DatabaseFactory));

        }
    }
}
