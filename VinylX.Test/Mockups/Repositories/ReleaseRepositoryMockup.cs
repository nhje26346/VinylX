using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class ReleaseRepositoryMockup : RepositoryMockupBase<Release>
    {
        public ReleaseRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(Release entity) => entity.ReleaseId;

        protected override void SetId(Release entity, int id) => entity.ReleaseId = id;
    }
}
