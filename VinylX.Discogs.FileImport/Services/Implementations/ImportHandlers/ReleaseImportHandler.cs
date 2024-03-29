﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers
{
    internal class ReleaseImportHandler : ImportHandlerBase
    {
        private readonly string[] releaseElements = new[] { "title", "labels", "released", "master_id", "identifiers", "formats", "genres" }; 

        protected override string ImportEndpointUrl => "https://prod-44.northeurope.logic.azure.com:443/workflows/0177f0116afa42a6b6a2331987794254/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=G4n7b5SnO1_SWjvus6xTBuObzYRNa0Fw7iVn_e69DUM";

        public ReleaseImportHandler(ILogger<ImportHandlerBase> logger) : base(logger)
        {
        }

        protected override bool IncludeElement(string prefix, string localName, string namespaceURI, int depth)
        {
            if (depth == 2)
            {
                // Elements below the release element
                return releaseElements.Contains(localName);
            }
            return true;
        }
    }
}
