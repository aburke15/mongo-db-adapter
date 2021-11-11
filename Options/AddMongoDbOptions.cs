namespace MongoDatabaseAdapter.Options;

public class AddMongoDbOptions
{
    private string? _connectionString;

    public void AddConnectionString(string connectionString)
        => _connectionString = connectionString;

    public string? GetConnectionString()
        => _connectionString;
}