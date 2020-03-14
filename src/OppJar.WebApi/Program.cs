using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OppJar.Common.Helpers;
using Serilog;
using Serilog.Events;

namespace OppJar.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .WriteTo.SQLite(@"Logs\log.db")
                            .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog();
                    webBuilder.UseEnvironment(EnvironmentHelper.Environment);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
