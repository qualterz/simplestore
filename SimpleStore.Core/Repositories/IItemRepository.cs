using SimpleStore.Core.Entities;

namespace SimpleStore.Core.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        void DeleteById(int itemId);
    }
}
