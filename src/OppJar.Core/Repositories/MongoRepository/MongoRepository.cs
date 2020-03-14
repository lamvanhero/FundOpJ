using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using OppJar.Mongo;

namespace OppJar.Core.Repositories
{
    public abstract class MongoRepository<TDocument> where TDocument : IDocument
    {
        //https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Driver/FilterDefinitionBuilder.cs#L1286
        public IMongoCollection<TDocument> Collection { get; private set; }

        private readonly MongoContext _context;

        public MongoRepository(MongoContext context, string collectionName)
        {
            _context = context;
            Collection = _context.GetCollection<TDocument>(collectionName);
        }

        public async Task AddAsync(TDocument document)
        {
            await Collection.InsertOneAsync(document);
        }

        public async Task AddManyAsync(IEnumerable<TDocument> documents)
        {
            await Collection.InsertManyAsync(documents);
        }

        public async Task UpdateAsync(TDocument document)
        {
            await Collection.ReplaceOneAsync(Filter(document.Id), document);
        }

        public async Task UpdateManyAsync(IEnumerable<TDocument> documents)
        {
            foreach (var document in documents)
            {
                await UpdateAsync(document);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(Filter(id));
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await Collection.DeleteOneAsync(Filter(id));
        }

        public async Task DeleteAsync(Expression<Func<TDocument, bool>> expression)
        {
            var doc = await Collection.Find(expression).FirstOrDefaultAsync();

            if (doc != null)
            {
                await DeleteAsync(doc.Id);
            }
        }

        public async Task DeleteManyAsync(FilterDefinition<TDocument> filter)
        {
            await Collection.DeleteManyAsync(filter);
        }

        public async Task<TDocument> FindByAsync(string id)
        {
            var result = await Collection.FindAsync(Filter(id));

            return result.FirstOrDefault();
        }

        public async Task<TDocument> FindByAsync(ObjectId id)
        {
            var result = await Collection.FindAsync(Filter(id));

            return result.FirstOrDefault();
        }

        public IFindFluent<TDocument, TDocument> Find(Expression<Func<TDocument, bool>> expression = null)
        {
            if (expression == null) return Collection.Find(x => true);

            return Collection.Find(Builders<TDocument>.Filter.Where(expression));
        }

        public FilterDefinition<TDocument> Filter(ObjectId id)
        {
            return Builders<TDocument>.Filter.Eq("Id", id);
        }

        public FilterDefinition<TDocument> Filter(Expression<Func<TDocument, bool>> expression)
        {
            return Builders<TDocument>.Filter.Where(expression);
        }

        public FilterDefinition<TDocument> Filter(string id)
        {
            return Filter(ObjectId.Parse(id));
        }

    }
}
