using Microsoft.Extensions.Caching.Memory;

namespace Validator.Infrastructure.Service;

public interface ICacheService
{
    T? GetFromCache<T>(string keyA, string KeyB);
    void AddToCache<T>(string keyA, string KeyB, T data, TimeSpan timeSpan);
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
}
