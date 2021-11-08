
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MongoDatabaseAdapter.Settings;

namespace MongoDatabaseAdapter.Abstractions
{
    public interface IMongoDbRepository
    {
        [UsedImplicitly]
        Task<IReadOnlyList<T>> GetAllAsync<T>(MongoDbConnectionSettings settings, CancellationToken ct = default) where T : class;
        [UsedImplicitly]
        Task<T> GetByIdAsync<T>(MongoDbConnectionSettings settings, string id, CancellationToken ct = default) where T: class;
        [UsedImplicitly]
        Task InsertOneAsync<T>(MongoDbConnectionSettings settings, T entity, CancellationToken ct = default) where T : class;
        [UsedImplicitly]
        Task InsertManyAsync<T>(MongoDbConnectionSettings settings, IEnumerable<T> entities, CancellationToken ct = default) where T : class;
    }
}