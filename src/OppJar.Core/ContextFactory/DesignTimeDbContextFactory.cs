using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Helpers;
using OppJar.Domain;
using System.IO;

namespace OppJar.Core.ContextFactory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OppJarContext>
    {
        public OppJarContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{EnvironmentHelper.Environment}.json", optional: true, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<OppJarContext>();

            var connectionString = configuration.GetConnectionString("DesignTimeDbConnectionString");

            builder.UseSqlServer(connectionString);

            return new OppJarContext(builder.Options);
        }
    }
}
