using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OppJar.Common.Factories;
using OppJar.Common.TokenSerializer;
using OppJar.Core.Services;
using OppJar.Domain;

namespace OppJar.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<IAccountService, AccountManager>();

            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                        .AddClasses()
                        .AsMatchingInterface()
                        .WithScopedLifetime();
            });

            ResolverFactory.SetProvider(services.BuildServiceProvider());

            return services;
        }
    }
}
