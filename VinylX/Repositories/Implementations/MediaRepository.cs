using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class MediaRepository : RepositoryBase<Media>
    {
        public MediaRepository(VinylXContext context) : base(context.Media) { }
    }
}
