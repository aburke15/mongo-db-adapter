using JetBrains.Annotations;

namespace MongoDatabaseAdapter.Settings
{
    [UsedImplicitly]
    public class MongoDbConnectionSettings
    {
        public string? DatabaseName { get; set; }
        public string? CollectionName { get; set; }
    }
}