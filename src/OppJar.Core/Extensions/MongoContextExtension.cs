using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OppJar.Core.ContextFactory;

namespace OppJar.Core.Extensions
{
    public static class MongoContextExtension
    {
        public static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((serviceProvider) =>
            {
                return new MongoFactory(configuration);
            });

            return services;
        }
    }
}
