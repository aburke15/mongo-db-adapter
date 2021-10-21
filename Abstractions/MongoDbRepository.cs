using System.Collections.Generic;
using System.Threading;
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

        public async Task<IEnumerable<T>> GetAllAsync<T>(string databaseName, string collectionName, CancellationToken ct = default)
        {
            ValidateMethodParameters(databaseName, collectionName);
            var db = _client.GetDatabase(databaseName);

            return await db.GetCollection<T>(collectionName)
                .Find(Builders<T>.Filter.Empty)
                .ToListAsync(ct);
        }

        public async Task InsertOneAsync<T>(T entity, string databaseName, string collectionName, CancellationToken ct = default)
        {
            ValidateMethodParameters(databaseName, collectionName);
            var db = _client.GetDatabase(databaseName);
            
            await db.GetCollection<T>(collectionName)
                .InsertOneAsync(document: entity, options: null, cancellationToken: ct);
        }

        private static void ValidateMethodParameters(string databaseName, string collectionName)
        {
            Guard.Against.NullOrWhiteSpace(databaseName, nameof(databaseName));
            Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));
        }
    }
}