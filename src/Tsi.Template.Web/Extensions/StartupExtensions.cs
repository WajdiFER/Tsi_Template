using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tsi.Template.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Tsi.Template.Core;
using System;
using Tsi.Template.Services;
using Tsi.Template.Helpers;
using Tsi.Template.Infrastructure;

namespace Tsi.Template.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            EngineContext.Create();

            LoadAssemblies();

            services.AddHttpContextAccessor();

            services.RegisterApplicationDependencies(Configuration);

            services.AddControllersWithViews();
        }

        private static void LoadAssemblies()
        {
            EngineContext.Current.LoadAssembly(typeof(StartupExtensions));
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerServices));
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerHelpers)); 
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerInfrastructure)); 
        }

        public static void ConfigureApplicationPipeline(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            EngineContext.Current.SetupServiceProvider(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
