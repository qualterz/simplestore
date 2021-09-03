using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;

namespace SimpleStore.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
