
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDatabaseAdapter.Abstractions
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string databaseName, string collectionName, CancellationToken ct = default);
        Task InsertOneAsync<T>(T entity, string databaseName, string collectionName, CancellationToken ct = default);
    }
}