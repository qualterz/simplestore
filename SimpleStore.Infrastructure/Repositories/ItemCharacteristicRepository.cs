using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;

namespace SimpleStore.Infrastructure.Repositories
{
    public class ItemCharacteristicRepository : Repository<ItemCharacteristic>, IItemCharacteristicRepository
    {
        public ItemCharacteristicRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public ItemCharacteristic Add(int itemId, int characteristicId)
        {
            var itemCharacteristic = new ItemCharacteristic()
            {
                ItemId = itemId,
                CharacteristicId = characteristicId,
            };

            return Add(itemCharacteristic);
        }

        public void Delete(int itemId, int characteristicId)
        {
            var itemCharacteristic = new ItemCharacteristic()
            {
                ItemId = itemId,
                CharacteristicId = characteristicId,
            };

            Delete(itemCharacteristic);
        }
    }
}
