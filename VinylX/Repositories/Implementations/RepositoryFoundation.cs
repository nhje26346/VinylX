
using VinylX.Data;

namespace VinylX.Repositories.Implementations
{
    internal class RepositoryFoundation : IRepositoryFoundation
    {
        private readonly VinylXContext context;

        public RepositoryFoundation(VinylXContext context)
        {
            this.context = context;
        }

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
