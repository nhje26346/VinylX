using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers
{
    internal class ArtistImportHandler : ImportHandlerBase
    {
        protected override string ImportEndpointUrl => "https://prod-61.northeurope.logic.azure.com:443/workflows/beabf98e8aa44443847ae2352e3b5e93/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=FNWb57b8DTccrEv-Q4cuDqpiEKKDqCIfFlvf0Uhv0gA";

        public ArtistImportHandler(ILogger<ImportHandlerBase> logger) : base(logger)
        {
        }
    }
}
