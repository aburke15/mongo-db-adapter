using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MongoDatabaseAdapter.Settings;
using MongoDB.Driver;

namespace MongoDatabaseAdapter.Abstractions;

public interface IMongoDbRepository
{
    [UsedImplicitly]
    Task<T> FindOneAsync<T>(MongoDbConnectionSettings settings, FilterDefinition<T> filter, CancellationToken ct = default) where T : class;
    
    [UsedImplicitly]
    Task<IReadOnlyList<T>> FindManyAsync<T>(MongoDbConnectionSettings settings, FilterDefinition<T> filter, CancellationToken ct = default) where T : class;
    
    [UsedImplicitly]
    Task<IReadOnlyList<T>> GetAllAsync<T>(MongoDbConnectionSettings settings, CancellationToken ct = default) where T : class;

    [UsedImplicitly]
    Task<T> GetByIdAsync<T>(MongoDbConnectionSettings settings, string key, CancellationToken ct = default) where T : class;

    [UsedImplicitly]
    Task InsertOneAsync<T>(MongoDbConnectionSettings settings, T document, CancellationToken ct = default) where T : class;

    [UsedImplicitly]
    Task InsertManyAsync<T>(MongoDbConnectionSettings settings, IEnumerable<T> documents, CancellationToken ct = default) where T : class;
}