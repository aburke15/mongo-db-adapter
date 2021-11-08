using JetBrains.Annotations;

namespace MongoDatabaseAdapter.Settings
{
    [UsedImplicitly]
    public class MongoDbConnectionSettings
    {
        [UsedImplicitly]
        public string? DatabaseName { get; set; }
        [UsedImplicitly]
        public string? CollectionName { get; set; }
    }
}