namespace ItemIntegration.Service.Common
{
    public enum ProviderType
    {
        MemCache,
        Redis
    }

    public enum CacheSetWhen
    {
        //
        // Summary:
        //     The operation should occur whether or not there is an existing value.
        Always,
        //
        // Summary:
        //     The operation should only occur when there is an existing value.
        Exists,
        //
        // Summary:
        //     The operation should only occur when there is not an existing value.
        NotExists
    }
}
