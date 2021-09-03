using SimpleStore.Core.Entities;
using System.Linq;

namespace SimpleStore.Core.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        IQueryable<TEntity> Entities { get; }

        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
