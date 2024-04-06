using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class RecordLabelRepositoryMockup : RepositoryMockupBase<RecordLabel>
    {
        public RecordLabelRepositoryMockup(IRepositoryFoundation repositoryFoundation) : base(repositoryFoundation)
        {
        }

        protected override int GetId(RecordLabel entity) => entity.RecordLabelId;

        protected override void SetId(RecordLabel entity, int id) => entity.RecordLabelId = id;
    }
}
