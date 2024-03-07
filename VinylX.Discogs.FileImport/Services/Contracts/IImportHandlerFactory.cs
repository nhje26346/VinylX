using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Discogs.FileImport.Entities;
using VinylX.Discogs.FileImport.Services.Implementations;

namespace VinylX.Discogs.FileImport.Services.Contracts
{
    internal interface IImportHandlerFactory
    {
        IImportHandler CreateImportHandler(EntityType entityType);
    }
}
