using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class FolderRepositoryMockup : RepositoryMockupBase<Folder>
    {
        public FolderRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(Folder entity) => entity.FolderId;

        protected override void SetId(Folder entity, int id) => entity.FolderId = id;
    }
}
