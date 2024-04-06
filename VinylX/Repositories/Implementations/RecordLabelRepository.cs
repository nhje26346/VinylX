using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class RecordLabelRepository : RepositoryBase<RecordLabel>
    {
        public RecordLabelRepository(VinylXContext context) : base(context.RecordLabel) { }
    }
}
