using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;

namespace SimpleStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
