using MongoDB.Driver;

namespace MongoDatabaseAdapter.Abstractions
{
    internal class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoClient _client;

        public MongoDbRepository(IMongoClient client)
        {
            _client = client;
        }
    }
}