using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OppJar.Core.Settings;
using ServiceStack.Stripe;

namespace OppJar.Core.Extensions
{
    public static class StripeExtension
    {
        public static IServiceCollection AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

            services.AddSingleton(o => new StripeGateway(configuration.GetValue<string>("Stripe:SecretKey")));

            return services;
        }
    }
}
