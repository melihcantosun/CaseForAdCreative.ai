namespace ItemIntegration.Service.Common
{
    public class AppSettings
    {
        public CacheSettings? CacheSettings { get; set; }
    }

    public class CacheSettings
    {
        public ProviderType? DefaultProviderType { get; set; }
        public string? DefaultRedisHostName { get; set; }
    }
    public class LockSettings
    {
        public ProviderType? DefaultLockProviderType { get; set; }
    }
}
