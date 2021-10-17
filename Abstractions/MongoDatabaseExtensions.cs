
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MongoDatabaseAdapter.Abstractions
{
    public static class MongoDatabaseExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}