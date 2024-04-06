using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class MasterReleaseRepositoryMockup : RepositoryMockupBase<MasterRelease>
    {
        public MasterReleaseRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(MasterRelease entity) => entity.MasterReleaseId;

        protected override void SetId(MasterRelease entity, int id) => entity.MasterReleaseId = id;
    }
}
