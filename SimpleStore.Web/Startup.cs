using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using SimpleStore.Application.Services;
using SimpleStore.Core.Repositories;
using SimpleStore.Infrastructure;
using SimpleStore.Infrastructure.Repositories;
using System;
using SimpleStore.Web.Areas.Store.Services;
using Microsoft.AspNetCore.Identity;

namespace SimpleStore.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSimpleStoreServices(services);

            ConfigureSession(services);

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();

            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

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
            services.AddScoped<IItemCharacteristicRepository, ItemCharacteristicRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion

            #region Application
            services.AddScoped<ICharacteristicService, CharacteristicService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region Web
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Web Administration

            #endregion

            #region Web Store
            services.AddScoped<ICartService, CartService>();
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
    }
}
