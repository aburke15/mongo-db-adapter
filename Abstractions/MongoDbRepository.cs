using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MongoDB.Driver;

namespace MongoDatabaseAdapter.Abstractions
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoClient _client;

        public MongoDbRepository(IMongoClient client) 
            => _client = Guard.Against.Null(client, nameof(client));

        public async Task<IEnumerable<T>> GetAllAsync<T>(string databaseName, string collectionName)
        {
            var db = _client.GetDatabase(databaseName);

            return await db.GetCollection<T>(collectionName)
                .Find(FilterDefinition<T>.Empty)
                .ToListAsync();
        }
    }
}