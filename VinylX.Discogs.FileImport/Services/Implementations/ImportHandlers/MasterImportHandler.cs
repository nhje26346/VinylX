using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers
{
    internal class MasterImportHandler : ImportHandlerBase
    {
        public MasterImportHandler(ILogger<ImportHandlerBase> logger) : base(logger)
        {
        }

        protected override string ImportEndpointUrl => "https://prod-34.northeurope.logic.azure.com:443/workflows/650900df58094a51a46008f729938b18/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=ydAG6zFWAqYrJ5ZThEVpfz7mOdiI5UJann0lwdVIci0";
    }
}
