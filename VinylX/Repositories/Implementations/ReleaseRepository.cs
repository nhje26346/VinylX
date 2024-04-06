using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class ReleaseRepository : RepositoryBase<Release>
    {
        public ReleaseRepository(VinylXContext context) : base(context.Release) { }
    }
}
