using Microsoft.Extensions.Configuration;
using Tsi.Template.Infrastructure.Data;

namespace Tsi.Template.Infrastructure.Repository
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        public ApplicationContext DataContext { get; }

        public DatabaseFactory(IConfiguration configuration)
        {
            DataContext = new ApplicationContext(configuration);
        }
        protected override void DisposeCore()
        {
            DataContext?.Dispose();
        }
    }

}
