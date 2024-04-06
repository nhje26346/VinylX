using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class UserRepositoryMockup : RepositoryMockupBase<User>
    {
        public UserRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(User entity) => entity.UserId;

        protected override void SetId(User entity, int id) => entity.UserId = id;
    }
}
