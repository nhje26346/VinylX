using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylX.Discogs.FileImport.Services.Contracts
{
    internal interface IImportHandler
    {
        Task ImportFile(string filename);
    }
}
