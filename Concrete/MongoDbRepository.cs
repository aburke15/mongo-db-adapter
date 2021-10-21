using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MongoDatabaseAdapter.Abstractions;
using MongoDatabaseAdapter.Settings;
using MongoDB.Driver;

namespace MongoDatabaseAdapter.Concrete
{
    public partial class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoClient _client;

        public MongoDbRepository(IMongoClient client) 
            => _client = Guard.Against.Null(client, nameof(client));

        public async Task<IEnumerable<T>> GetAllAsync<T>(MongoDbConnectionSettings connectionSettings, CancellationToken ct = default) where T : class
        {
            var (databaseName, collectionName) = ValidateMethodParameters(connectionSettings);
            var db = _client.GetDatabase(databaseName);

            return await db.GetCollection<T>(collectionName)
                .Find(Builders<T>.Filter.Empty)
                .ToListAsync(ct);
        }

        public async Task InsertOneAsync<T>(MongoDbConnectionSettings connectionSettings, T entity, CancellationToken ct = default) where T : class
        {
            var (databaseName, collectionName) = ValidateMethodParameters(connectionSettings);
            var db = _client.GetDatabase(databaseName);
            
            await db.GetCollection<T>(collectionName)
                .InsertOneAsync(document: entity, options: null, cancellationToken: ct);
        }

        private static (string databaseName, string collectionName) ValidateMethodParameters(MongoDbConnectionSettings connectionSettings)
        {
            Guard.Against.Null(connectionSettings, nameof(connectionSettings));
            Guard.Against.NullOrWhiteSpace(connectionSettings.DatabaseName, nameof(connectionSettings.DatabaseName));
            Guard.Against.NullOrWhiteSpace(connectionSettings.CollectionName, nameof(connectionSettings.CollectionName));

            return (connectionSettings.DatabaseName, connectionSettings.CollectionName);
        }
    }
}