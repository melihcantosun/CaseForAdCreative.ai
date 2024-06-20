using ItemIntegration.Interface;
using ItemIntegration.Service.Common;
using ItemIntegration.Service.Interface;
using ItemIntegration.Service.Provider.Cache;

namespace ItemIntegration.Provider.Lock
{
    public class LockProvider : ILockProvider
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly int @defaultLockExpiryInSeconds = 60;
        public LockProvider(AppSettings appSettings)
        {
            _cacheProvider = new CacheProvider(appSettings.CacheSettings);
        }
        public bool Lock(string key, TimeSpan? expiry = null) => _cacheProvider.SetKeyValue(key, "lock", expiry ?? TimeSpan.FromSeconds(@defaultLockExpiryInSeconds), CacheSetWhen.NotExists);
        public Task<bool> Unlock(string key) => _cacheProvider.DeleteKeyAsync(key);
    }
}
