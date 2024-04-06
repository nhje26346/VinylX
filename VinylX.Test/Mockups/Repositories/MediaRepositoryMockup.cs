using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class MediaRepositoryMockup : RepositoryMockupBase<Media>
    {
        public MediaRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(Media entity) => entity.MediaID;

        protected override void SetId(Media entity, int id) => entity.MediaID = id;
    }
}
