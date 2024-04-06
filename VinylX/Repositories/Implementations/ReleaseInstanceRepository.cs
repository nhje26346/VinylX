using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class ReleaseInstanceRepository : RepositoryBase<ReleaseInstance>
    {
        public ReleaseInstanceRepository(VinylXContext context) : base(context.ReleaseInstance) { }
    }
}
