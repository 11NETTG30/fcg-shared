using FCG.Shared.Domain.Application;
using FCG.Shared.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FCG.Shared.Infrastructure.Configurations;

public static class CacheConfiguration
{
    public static IServiceCollection AddFcgRedisCache(
        this IServiceCollection services,
        IConfiguration configuration,
        string instanceName)
    {
        var connectionString = configuration.GetConnectionString("Redis")
            ?? "localhost:6379";

        var prefixWithColon = instanceName.EndsWith(':') ? instanceName : $"{instanceName}:";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
            options.InstanceName = prefixWithColon;
        });

        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(connectionString));

        services.AddScoped<ICacheService>(sp =>
            new RedisCacheService(
                sp.GetRequiredService<IDistributedCache>(),
                sp.GetRequiredService<IConnectionMultiplexer>(),
                prefixWithColon));

        return services;
    }
}
