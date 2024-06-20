namespace ItemIntegration.Interface
{
    public interface ILockProvider
    {
        /// <summary>
        /// Creates a lock with the specified key.
        /// </summary>
        /// <param name="key">The key to be locked.</param>
        /// <param name="expiry">Optional time span indicating the duration of the lock.</param>
        /// <returns>True if the lock was successfully acquired, otherwise false.</returns>
        bool Lock(string key, TimeSpan? expiry = null);

        /// <summary>
        /// Releases the lock with the specified key.
        /// </summary>
        /// <param name="key">The key to be unlocked.</param>
        /// <returns>A task that represents the asynchronous unlock operation. The task result contains true if the lock was successfully released, otherwise false.</returns>
        Task<bool> Unlock(string key);
    }
}
