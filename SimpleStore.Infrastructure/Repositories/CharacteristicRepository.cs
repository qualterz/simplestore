using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;

namespace SimpleStore.Infrastructure.Repositories
{
    public class CharacteristicRepository : Repository<Characteristic>, ICharacteristicRepository
    {
        public CharacteristicRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
