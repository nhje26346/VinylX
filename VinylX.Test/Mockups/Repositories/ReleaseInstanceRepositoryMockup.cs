using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class ReleaseInstanceRepositoryMockup : RepositoryMockupBase<ReleaseInstance>
    {
        public ReleaseInstanceRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(ReleaseInstance entity) => entity.ReleaseInstanceId;

        protected override void SetId(ReleaseInstance entity, int id) => entity.ReleaseInstanceId = id;
    }
}
