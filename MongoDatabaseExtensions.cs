using System;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDatabaseAdapter.Abstractions;
using MongoDatabaseAdapter.Repositories;
using MongoDatabaseAdapter.Options;
using MongoDB.Driver;

namespace MongoDatabaseAdapter;

[UsedImplicitly]
public static class MongoDatabaseExtensions
{
    // TODO: convert this to a cqrs style library
    /// <summary>
    /// Adds MongoDb and MongoDbRepository to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    [UsedImplicitly]
    public static IServiceCollection AddMongoDb(
        this IServiceCollection services,
        Action<AddMongoDbOptions> setupAction)
    {
        services = Guard.Against
            .Null(services, nameof(services));
        setupAction = Guard.Against
            .Null(setupAction, nameof(setupAction));

        services.AddOptions();
        services.Configure(setupAction);

        services.AddSingleton<IMongoClient, MongoClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<AddMongoDbOptions>>().Value;
            var connectionString = options?.GetConnectionString();

            connectionString = Guard.Against
                .NullOrWhiteSpace(connectionString, nameof(connectionString));

            //TODO: Provide more connection options
            return new MongoClient(connectionString);
        });

        services.AddScoped<IMongoDbRepository, MongoDbRepository>();
        services.AddMediatR(typeof(MongoDatabaseExtensions));

        return services;
    }
}