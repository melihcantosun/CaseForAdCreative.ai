using ItemIntegration.Service.Common;
using ItemIntegration.Service.Backend;
using ItemIntegration.Service.Provider.Items;

namespace ItemIntegration.Service
{
    public class ItemIntegrationService
    {
        ItemIntegrationBackend _backendService;
        public ItemIntegrationService(ItemProvider itemProvider)
        {
            _backendService = new ItemIntegrationBackend(itemProvider);
        }

        public Result SaveItem(string itemContent)
        {
            //Send content to backend service
            return _backendService.SaveItem(itemContent);
        }
        public List<Item> GetAllItems() => _backendService.GetAllItems();
    }
}
