using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;

namespace SimpleStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
