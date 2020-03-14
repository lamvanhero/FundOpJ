using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stream;

namespace OppJar.Core.Extensions
{
    public static class GetStreamExtension
    {
        public static IServiceCollection AddGetStreamClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(o => new StreamClient(configuration.GetSection("GetStream").GetValue<string>("ApiKey"), configuration.GetSection("GetStream").GetValue<string>("Secret")));

            return services;
        }
    }
}
