using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
using System.Linq;

namespace SimpleStore.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {

        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;

            dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Entities => dbSet;

        public TEntity Add(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
    }
}
