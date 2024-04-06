namespace VinylX.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }

        TEntity Add(TEntity entity);
        ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        
    }
}
