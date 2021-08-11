using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tsi.Template.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Tsi.Template.Core;
using Tsi.Template.Services;
using Tsi.Template.Helpers;
using Tsi.Template.Infrastructure;
using FluentValidation.AspNetCore;
using Tsi.Template.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

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

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidatorAssemblyReferencer>());

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme, (options) =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        private static void LoadAssemblies()
        {
            EngineContext.Current.LoadAssembly(typeof(StartupExtensions));
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerServices));
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerHelpers));
            EngineContext.Current.LoadAssembly(typeof(AssemblyReferencerInfrastructure));
            EngineContext.Current.LoadAssembly(typeof(ValidatorAssemblyReferencer));
            EngineContext.Current.LoadAssembly(typeof(EngineContext));
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

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapAreaControllerRoute("AdminRoutes", "Admin", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
