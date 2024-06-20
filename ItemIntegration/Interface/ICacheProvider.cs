using ItemIntegration.Service.Common;

namespace ItemIntegration.Service.Interface
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Sets a key-value pair in the cache with an optional expiration and condition.
        /// </summary>
        /// <param name="key">The key under which the value will be stored.</param>
        /// <param name="value">The value to be stored in the cache.</param>
        /// <param name="expiry">An optional expiration time for the key-value pair. If null, the key does not expire.</param>
        /// <param name="cacheSetWhen">An optional condition indicating when the value should be set.</param>
        /// <returns>A boolean indicating whether the operation was successful.</returns>
        bool SetKeyValue(string key, string value, TimeSpan? expiry, CacheSetWhen cacheSetWhen = CacheSetWhen.Always);

        /// <summary>
        /// Asynchronously deletes a key from the cache.
        /// </summary>
        /// <param name="key">The key to be deleted from the cache.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean result indicating whether the deletion was successful.</returns>
        Task<bool> DeleteKeyAsync(string key);
    }
}
