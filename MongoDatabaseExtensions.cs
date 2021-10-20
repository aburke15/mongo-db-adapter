
using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDatabaseAdapter.Abstractions;
using MongoDatabaseAdapter.Options;
using MongoDB.Driver;

namespace MongoDatabaseAdapter
{
    public static class MongoDatabaseExtensions
    {
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<AddMongoDbOptions> setupAction)
        {
            services = Guard.Against.Null(services, nameof(services));
            setupAction = Guard.Against.Null(setupAction, nameof(setupAction));

            services
                .AddOptions()
                .Configure(setupAction);

            services.AddScoped<IMongoClient, MongoClient>(provider => 
            {
                var options = provider.GetRequiredService<IOptions<AddMongoDbOptions>>().Value;
                var connectionString = options?.GetConnectionString();

                connectionString = Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));

                // TODO: Provide more connection options
                return new MongoClient(connectionString);
            });

            services.AddScoped<IMongoDbRepository, MongoDbRepository>();
            
            return services;
        }
    }
}