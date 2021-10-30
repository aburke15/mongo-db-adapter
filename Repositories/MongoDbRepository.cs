using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MongoDatabaseAdapter.Abstractions;
using MongoDatabaseAdapter.Settings;
using MongoDB.Driver;

namespace MongoDatabaseAdapter.Repositories
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoClient _client;

        public MongoDbRepository(IMongoClient client) 
            => _client = Guard.Against.Null(client, nameof(client));

        public async Task<IEnumerable<T>> GetAllAsync<T>(
            MongoDbConnectionSettings settings, 
            CancellationToken ct = default) where T : class
        {
            var (databaseName, collectionName) = ValidateMethodParameters(settings);

            var databaseCollection = await GetCollectionIfExistsAsync<T>(
                await GetDatabaseIfExistsAsync(databaseName, ct), collectionName, ct);

            return await databaseCollection
                .Find(Builders<T>.Filter.Empty)
                .ToListAsync(ct);
        }

        public Task<T> GetByIdAsync<T>( // TODO: add some type of filter as a param
            MongoDbConnectionSettings settings, 
            string id, CancellationToken ct = default) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task InsertOneAsync<T>(
            MongoDbConnectionSettings settings, 
            T entity, CancellationToken ct = default) where T : class
        {
            var (databaseName, collectionName) = ValidateMethodParameters(settings);
            
            var databaseCollection = await GetCollectionIfExistsAsync<T>(
                await GetDatabaseIfExistsAsync(databaseName, ct), collectionName, ct);

            await databaseCollection.InsertOneAsync(entity, null, ct);
        }

        public async Task InsertManyAsync<T>(
            MongoDbConnectionSettings settings, 
            IEnumerable<T> entities, 
            CancellationToken ct = default) where T : class
        {
            var (databaseName, collectionName) = ValidateMethodParameters(settings);

            var databaseCollection = await GetCollectionIfExistsAsync<T>(
                await GetDatabaseIfExistsAsync(databaseName, ct), collectionName, ct);

            await databaseCollection.InsertManyAsync(entities, null, ct);
        }

        private static (string databaseName, string collectionName) ValidateMethodParameters(
            MongoDbConnectionSettings connectionSettings)
        {
            Guard.Against.Null(connectionSettings, nameof(connectionSettings));

            var databaseName = connectionSettings.DatabaseName;
            var collectionName = connectionSettings.CollectionName;

            Guard.Against.NullOrWhiteSpace(databaseName, nameof(databaseName));
            Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));

            return (databaseName, collectionName);
        }

        private async Task<IMongoDatabase> GetDatabaseIfExistsAsync(string databaseName, CancellationToken ct)
        {
            var results = await _client.ListDatabaseNamesAsync(ct);
            var databaseNames = await results.ToListAsync(ct);

            if (!databaseNames.Contains(databaseName))
                throw new ArgumentException(message: "Supplied database name does not exist.");

            return _client.GetDatabase(databaseName);
        }

        private static async Task<IMongoCollection<T>> GetCollectionIfExistsAsync<T>(
            IMongoDatabase mongoDatabase, string collectionName, CancellationToken ct) where T : class
        {
            var results = await mongoDatabase.ListCollectionNamesAsync(null, ct);
            var collectionNames = await results.ToListAsync(ct);

            if (!collectionNames.Contains(collectionName))
                throw new ArgumentException("Supplied collection name does not exist.");

            return mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}