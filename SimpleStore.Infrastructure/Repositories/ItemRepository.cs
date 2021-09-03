using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
using System.Linq;

namespace SimpleStore.Infrastructure.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void DeleteById(int itemId)
        {
            var item = Entities.Single(e => e.ItemId == itemId);
            Delete(item);
        }   
    }
}