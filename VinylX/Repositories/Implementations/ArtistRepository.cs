using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class ArtistRepository : RepositoryBase<Artist>
    {
        public ArtistRepository(VinylXContext context) : base(context.Artist) { }
    }
}
