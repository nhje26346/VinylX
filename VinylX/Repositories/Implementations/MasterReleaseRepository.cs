using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class MasterReleaseRepository : RepositoryBase<MasterRelease>
    {
        public MasterReleaseRepository(VinylXContext context) : base(context.MasterRelease) { }
    }
}
