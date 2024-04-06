using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class ArtistRepositoryMockup : RepositoryMockupBase<Artist>
    {
        public ArtistRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(Artist entity) => entity.ArtistId;

        protected override void SetId(Artist entity, int id) => entity.ArtistId = id;
    }
}
