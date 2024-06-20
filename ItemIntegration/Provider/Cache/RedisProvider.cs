using ItemIntegration.Service.Common;
using ItemIntegration.Service.Interface;
using StackExchange.Redis;

namespace ItemIntegration.Service.Provider.Cache
{
    public class RedisProvider : IRedisProvider
    {
        private readonly string _connectionString = "localhost:6379";
        private readonly IDatabase _database;
        public RedisProvider(string connectionString)
        {
            _connectionString = !string.IsNullOrEmpty(connectionString) ? connectionString : _connectionString;
            _database ??= ConnectionMultiplexer.Connect(_connectionString).GetDatabase();
        }

        public bool SetKeyValue(string key, string value, TimeSpan? expiry, CacheSetWhen cacheSetWhen)
        {
            var when = cacheSetWhen switch
            {
                CacheSetWhen.Always => When.Always,
                CacheSetWhen.Exists => When.Exists,
                CacheSetWhen.NotExists => When.NotExists,
                _ => throw new ArgumentOutOfRangeException(nameof(cacheSetWhen), cacheSetWhen, null)
            };

            return _database.StringSet(key, value, expiry, when);
        }
        public Task<bool> DeleteKeyAsync(string key) => _database.KeyDeleteAsync(key);
    }
}
