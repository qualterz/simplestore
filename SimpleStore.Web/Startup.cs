using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleStore.Application.Services;
using SimpleStore.Core.Repositories;
using SimpleStore.Infrastructure;
using SimpleStore.Infrastructure.Repositories;
using System;
using SimpleStore.Web.Areas.Store.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SimpleStore.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSimpleStoreServices(services);
            ConfigureSession(services);
            ConfigureAuthentication(services);

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();

            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "Store",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapAreaControllerRoute(
                    name: "administration",
                    areaName: "Administration",
                    pattern: "Administration/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapAreaControllerRoute(
                    name: "account",
                    areaName: "Account",
                    pattern: "Account/{controller=Login}/{action=Index}/{id?}"
                );
            });

            var cookiePolicyOptions = new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            app.UseCookiePolicy(cookiePolicyOptions);
        }

        public void ConfigureSimpleStoreServices(IServiceCollection services)
        {
            ConfigureDatabases(services);

            #region Infrastructure
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemCharacteristicRepository, ItemCharacteristicRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion

            #region Application
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICharacteristicService, CharacteristicService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region Web
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IItemControllerService, ItemControllerService>();
            #endregion

            #region Web Administration
            #endregion

            #region Web Store
            services.AddScoped<ICartControllerService, CartControllerService>();
            #endregion
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=simplestore.db");
                options.UseLazyLoadingProxies();
            });
        }

        public void ConfigureSession(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            services.AddAuthentication(scheme)
                .AddCookie();
        }
    }
}
