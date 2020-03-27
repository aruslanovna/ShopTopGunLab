using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<NorthwindDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("NorthwindDatabase")));

          //  services.AddScoped<IShopDbContext>(provider => provider.GetService<ShopDbContext>());
            // services.AddScoped<INorthwindDbContext>(provider => provider.GetService<ApplicationDbContext>());
            return services;
        }
    }
}
