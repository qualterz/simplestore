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

namespace SimpleStore.Web.Admin
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSimpleStoreServices(services);

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        public void ConfigureSimpleStoreServices(IServiceCollection services)
        {
            ConfigureDatabases(services);

            #region Infrastructure
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion

            #region Application
            services.AddScoped<ICharacteristicService, CharacteristicService>();
            services.AddScoped<IItemService, ItemService>();
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
    }
}