using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace Validator.Infrastructure.Cache;

public interface ICacheService
{
    T? GetFromCache<T>(string keyA, string KeyB);
    void AddToCache<T>(string keyA, string KeyB, T data, TimeSpan timeSpan);
    Task<T> GetOrSetCache<T>(string keyA, string keyB, Func<Task<T>> Func, TimeSpan timeSpan);
}

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void AddToCache<T>(string keyA, string KeyB, T data, TimeSpan timeSpan)
    {
        var key = keyA + "." + KeyB;
        _memoryCache.Set<T>(key, data, timeSpan);
    }

    public T? GetFromCache<T>(string keyA, string KeyB)
    {
        var key = keyA + "." + KeyB;
        _memoryCache.TryGetValue(key, out T result);

        return result;
    }

    public async Task<T> GetOrSetCache<T>(string keyA, string keyB, Func<Task<T>> Func, TimeSpan timeSpan)
    {
        var key = keyA + "." + keyB;
        if (!_memoryCache.TryGetValue(key, out T cachedData))
        {
            cachedData = await Func();
            _memoryCache.Set(key, cachedData, timeSpan);
        }

        return cachedData;
    }
}