
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDatabaseAdapter.Abstractions
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string databaseName, string collectionName);
    }
}