using SimpleStore.Core.Entities;

namespace SimpleStore.Core.Repositories
{
    public interface IItemCharacteristicRepository : IRepository<ItemCharacteristic>
    {
        ItemCharacteristic Add(int itemId, int characteristicId);
        void Delete(int itemId, int characteristicId);
    }
}
