using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OppJar.Web.Background.Queues;
using OppJar.Web.Proxies;

namespace OppJar.Web
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
            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                        .AddClasses()
                        .AsMatchingInterface();
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();

            services.AddHttpClient<OppJarProxy>("OppJarProxy", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("OppJarProxyUrl"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
            {
                opts.AccessDeniedPath = new PathString("/Error/403");
                opts.LoginPath = new PathString("/Account/Signin");
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("AreaAdmin", "Admins/{controller=Home}/{action=Index}/{id?}",
                    defaults: new { area = "Admins" }, constraints: new { area = "Admins" });

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "timeline",
                    template: "user/{slug}",
                    defaults: new { controller = "Profile", action = "Index" });
            });
        }
    }
}
