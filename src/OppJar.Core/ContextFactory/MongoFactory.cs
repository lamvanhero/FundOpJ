using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OppJar.Mongo;

namespace OppJar.Core.ContextFactory
{
    public class MongoFactory
    {
        public IMongoDatabase Database { get; private set; }

        public MongoFactory(IConfiguration configuration)
        {
            var mongoClient = new MongoContext(configuration.GetSection("MongoSettings").GetValue<string>("ConnectionString"),
                configuration.GetSection("MongoSettings").GetValue<string>("DbName"));

            Database = mongoClient.Database;
        }
    }
}
