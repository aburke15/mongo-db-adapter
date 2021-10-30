
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MongoDatabaseAdapter.Settings;

namespace MongoDatabaseAdapter.Abstractions
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(MongoDbConnectionSettings settings, CancellationToken ct = default) where T : class;
        Task<T> GetByIdAsync<T>(MongoDbConnectionSettings settings, string id, CancellationToken ct = default) where T: class;
        Task InsertOneAsync<T>(MongoDbConnectionSettings settings, T entity, CancellationToken ct = default) where T : class;
        Task InsertManyAsync<T>(MongoDbConnectionSettings settings, IEnumerable<T> entities, CancellationToken ct = default) where T : class;
    }
}