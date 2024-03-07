using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Discogs.FileImport.Services.Contracts;

namespace VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers
{
    internal class LabelImportHandler : ImportHandlerBase
    {
        protected override string ImportEndpointUrl => "https://prod-44.northeurope.logic.azure.com:443/workflows/6cc12d586b494f9d8f90f16b345957d4/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=fF3dC9aZAjpsI6wFsEzY0Tl6QvjdD6A6jzfzDSkfkZo";

        public LabelImportHandler(ILogger<ImportHandlerBase> logger) : base(logger)
        {
        }
    }
}
