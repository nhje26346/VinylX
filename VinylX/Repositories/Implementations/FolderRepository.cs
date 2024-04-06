using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class FolderRepository : RepositoryBase<Folder>
    {
        public FolderRepository(VinylXContext context) : base(context.Folder) { }
    }
}
