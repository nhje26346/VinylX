using Microsoft.EntityFrameworkCore;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Repositories.Implementations
{
    internal class UserRepository : RepositoryBase<User>
    {
        public UserRepository(VinylXContext context) : base(context.User) { }
    }
}
