using ItemIntegration.Interface;
using System.Collections.Concurrent;

namespace ItemIntegration.Service.Provider.Items
{
    public class ItemProvider
    {
        readonly ILockProvider _lockProvider;
        private int _identitySequence;
        private ConcurrentBag<Item> SavedItems { get; set; } = new();

        public ItemProvider(ILockProvider lockProvider)
        {
            _lockProvider = lockProvider;
        }

        public Item SaveItem(Item item)
        {
            Thread.Sleep(2_000);
            item.Id = GetNextIdentity();
            SavedItems.Add(item);
            return item;
        }
        private int GetNextIdentity() => Interlocked.Increment(ref _identitySequence);
        public List<Item> GetAllItems() => SavedItems.ToList();
        public bool Lock(string key, TimeSpan? expiry = null) => _lockProvider.Lock(key, expiry);
        public Task<bool> Unlock(string key) => _lockProvider.Unlock(key);
    }
}
