using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OppJar.Domain;
using System;

namespace OppJar.Core.Extensions
{
    public static class DbContextExtension
    {
        private static IUnitOfWork UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            return new UnitOfWork(serviceProvider.GetService<OppJarContext>(), serviceProvider.GetService<IHttpContextAccessor>());
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OppJarContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpContextAccessor();

            services.AddTransient<DbSeed>();

            services.AddScoped(UnitOfWorkFactory);

            return services;
        }
    }
}
