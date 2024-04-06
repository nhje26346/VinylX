
using Microsoft.EntityFrameworkCore;
using System.Data;
using VinylX.Data;

namespace VinylX.Repositories
{
    internal abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        public IQueryable<TEntity> Queryable => dbSet;

        public RepositoryBase(DbSet<TEntity> dbSet)
        {
            this.dbSet = dbSet;
        }

        public TEntity Add(TEntity entity) => dbSet.Add(entity).Entity;

        public ValueTask<TEntity?> FindAsync(params object?[]? keyValues) => dbSet.FindAsync(keyValues);

        public TEntity Remove(TEntity entity) => dbSet.Remove(entity).Entity;

        public TEntity Update(TEntity entity) => dbSet.Update(entity).Entity;
    }
}
