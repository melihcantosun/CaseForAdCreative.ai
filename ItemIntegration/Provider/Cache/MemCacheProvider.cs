using ItemIntegration.Service.Common;
using ItemIntegration.Service.Interface;
using System.Runtime.Caching;

namespace ItemIntegration.Service.Provider.Cache
{
    public class MemCacheProvider : ICacheProvider
    {
        private static MemoryCache cache = MemoryCache.Default;
        private readonly DateTimeOffset _expiry = DateTimeOffset.UtcNow.AddMinutes(2);
        public MemCacheProvider() { }
        public MemCacheProvider(string cacheGroup)
        {
            cache = new MemoryCache(cacheGroup);
        }

        public bool SetKeyValue(string key, string value, TimeSpan? expiry, CacheSetWhen cacheSetWhen)
        {
            DateTimeOffset expiration = expiry.HasValue ? DateTime.Now.Add(expiry.Value) : _expiry;
            bool addKeyValue = cacheSetWhen switch
            {
                CacheSetWhen.Always => true,
                CacheSetWhen.Exists => cache.Get(key) != null,
                CacheSetWhen.NotExists => cache.Get(key) == null,
                _ => false
            };
            return addKeyValue && cache.Add(key, value, expiration);
        }
        public Task<bool> DeleteKeyAsync(string key) => Task.FromResult(cache.Remove(key) != null);
    }
}
