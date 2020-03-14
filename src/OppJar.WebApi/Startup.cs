using CSharpEmail.Extensions;
using FluentValidation.AspNetCore;
using JwtTokenServer.Extensions;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OppJar.Core.Extensions;
using OppJar.Core.Services;
using OppJar.Domain;
using OppJar.Dto;
using Serilog;
using Stripe;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace OppJar.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJWTBearerToken(Configuration);

            services.AddAccountManager<AccountManager>();

            services.AddContext(Configuration);

            services.AddMongoContext(Configuration);

            services.AddServices();

            services.AddBackgroundService();

            services.AddStripe(Configuration);

            services.UseCSharpSmtpClient(Configuration);

            services.AddHttpClient<OAuthClient>(typeof(OAuthClient).Name, client => client.BaseAddress = new Uri(Configuration.GetSection("JWTSettings").GetValue<string>("Issuer")));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                })
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    })
                .AddFluentValidation(config =>
                    {
                        config.RegisterValidatorsFromAssembly(typeof(RegisterDtoAbstract).Assembly);
                        config.ImplicitlyValidateChildProperties = true;
                    }
                );

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe").GetValue<string>("SecretKey");

            services.AddCors();

            services.AddSwashbuckle();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OppJarContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.Database.Migrate();

            app.AddLog(env);

            app.UseSerilogRequestLogging();

            app.UseSwashbuckle();

            app.UseJWTBearerToken(Configuration);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets")),
                RequestPath = "/assets"
            });

            var origins = Configuration.GetValue<string>("Cors");

            app.UseCors(builder =>
            {
                builder.WithOrigins(origins.Split(","));
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
