namespace VinylX.Repositories
{
    public interface IRepositoryFoundation
    {
        Task<int> SaveChangesAsync();
    }
}
