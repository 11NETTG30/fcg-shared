using System.Text.Json;
using FCG.Shared.Domain.Application;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace FCG.Shared.Infrastructure.Cache;

public class RedisCacheService(IDistributedCache cache, IConnectionMultiplexer redis, string instanceName) : ICacheService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var data = await cache.GetStringAsync(key, cancellationToken);
        return data is null ? default : JsonSerializer.Deserialize<T>(data, JsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
        };

        var data = JsonSerializer.Serialize(value, JsonOptions);
        await cache.SetStringAsync(key, data, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var db = redis.GetDatabase();
        var server = redis.GetServer(redis.GetEndPoints().First());

        // InstanceName é prefixado automaticamente pelo IDistributedCache — necessário incluí-lo no SCAN
        var keys = server.Keys(pattern: $"{instanceName}{prefix}*").ToArray();
        if (keys.Length > 0)
            await db.KeyDeleteAsync(keys);
    }
}
