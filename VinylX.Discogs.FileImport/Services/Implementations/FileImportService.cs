using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Discogs.FileImport.Services.Contracts;

namespace VinylX.Discogs.FileImport.Services.Implementations
{
    internal class FileImportService
    {
        private readonly ILogger<FileImportService> logger;
        private readonly EntityService entityService;
        private readonly IImportHandlerFactory importHandlerFactory;
        private readonly ZipFileService zipFileService;

        public FileImportService(ILogger<FileImportService> logger, EntityService entityService, IImportHandlerFactory importHandlerFactory, ZipFileService zipFileService)
        {
            this.logger = logger;
            this.entityService = entityService;
            this.importHandlerFactory = importHandlerFactory;
            this.zipFileService = zipFileService;
        }

        public async Task RunImport(string[] args)
        {
            var filename = args.FirstOrDefault() ?? throw new Exception("Missing import filename argument!");
            filename = await zipFileService.UnzipIfZipped(filename);
            var entityType = entityService.GetEntityTypeFromFilename(filename);
            var importHandler = importHandlerFactory.CreateImportHandler(entityType);
            await importHandler.ImportFile(filename);

            logger.LogInformation("DONE!");
        }
    }
}
