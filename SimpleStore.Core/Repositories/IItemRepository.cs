using SimpleStore.Core.Entities;

namespace SimpleStore.Core.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetById(int itemId);
        void DeleteById(int itemId);
    }
}
