﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Linq;

namespace OppJar.Mongo
{
    public class MongoContext : MongoClient
    {
        public IMongoDatabase Database { get; private set; }

        public MongoContext(string connectionString, string dbName) : base(connectionString)
        {
            var pack = new ConventionPack()
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);

            Database = GetDatabase(dbName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
            where TDocument : IDocument
        {
            if (!CollectionExists(name))
            {
                Database.CreateCollection(name);
            }

            return Database.GetCollection<TDocument>(name);
        }

        public bool CollectionExists(string name)
        {
            var filter = new BsonDocument("name", name);

            var collections = Database.ListCollections(new ListCollectionsOptions { Filter = filter });

            return (collections.ToList()).Any();
        }
    }
}
