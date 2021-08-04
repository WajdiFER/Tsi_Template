using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Tsi.Template.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {

        private readonly IConfiguration Configuration;
        public ApplicationContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
           .UseSqlServer(Configuration.GetConnectionString("Default"));
            //Enable this so entity framework will bring navigation variables when called!
            //.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }


        public Task<int> CommitAsync()
        {
            return SaveChangesAsync();
        }
    }
}
