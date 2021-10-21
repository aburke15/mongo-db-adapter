
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDatabaseAdapter.Settings;

namespace MongoDatabaseAdapter.Abstractions
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(MongoDbConnectionSettings connectionSettings, CancellationToken ct = default) where T : class;

        Task InsertOneAsync<T>(MongoDbConnectionSettings connectionSettings, T entity, CancellationToken ct = default) where T : class;
    }
}