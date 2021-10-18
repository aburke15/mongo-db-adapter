using Microsoft.Extensions.Configuration;

namespace MongoDatabaseAdapter.Options
{
    public class AddMongoDbOptions
    {
        public string ConnectionString { get; private set; }
        
        public void AddConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}