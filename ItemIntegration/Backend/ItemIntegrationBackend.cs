using ItemIntegration.Service.Common;
using ItemIntegration.Service.Provider.Items;

namespace ItemIntegration.Service.Backend
{
    public class ItemIntegrationBackend
    {
        readonly ItemProvider _itemProvider;

        public ItemIntegrationBackend(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }

        public Result SaveItem(string itemContent)
        {
            //Lock the item on the caching mechanism.
            if (!_itemProvider.Lock(itemContent)) return new Result(false, $"Duplicate item received with content {itemContent}.");
            var item = new Item() { Content = itemContent };
            try
            {
                //Save the unique item
                item = _itemProvider.SaveItem(item);
            }
            catch
            {
                //If the process fails, remove the lock and throw error.
                _itemProvider.Unlock(itemContent);
                throw;
            }
            return new Result(true, $"Item with content {itemContent} saved with id {item.Id}");
        }
        public List<Item> GetAllItems() => _itemProvider.GetAllItems().OrderBy(o => o.Id).ToList();
    }
}
