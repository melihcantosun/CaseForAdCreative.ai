using ItemIntegration.Service.Common;
using ItemIntegration.Service.Interface;

namespace ItemIntegration.Service.Provider.Cache
{
    public class CacheProvider : ICacheProvider
    {
        readonly ProviderType @DefaultProviderType = ProviderType.Redis;
        public ICacheProvider _cacheService;
        public CacheProvider(CacheSettings? cacheSettings)
        {
            _cacheService = (cacheSettings?.DefaultProviderType ?? @DefaultProviderType) switch
            {
                ProviderType.MemCache => new MemCacheProvider(),
                ProviderType.Redis => new RedisProvider(cacheSettings?.DefaultRedisHostName ?? string.Empty),
                _ => throw new InvalidCastException("The given 'ProviderType' is invalid.")
            };
        }

        public Task<bool> DeleteKeyAsync(string key) => _cacheService.DeleteKeyAsync(key);
        public bool SetKeyValue(string key, string value, TimeSpan? expiry, CacheSetWhen cacheSetWhen) => _cacheService.SetKeyValue(key, value, expiry, cacheSetWhen);
    }
}
