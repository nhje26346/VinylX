using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Discogs.FileImport.Entities;
using VinylX.Discogs.FileImport.Services.Contracts;

namespace VinylX.Discogs.FileImport.Services.Implementations
{
    internal class ImportHandlerFactory : IImportHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ImportHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IImportHandler CreateImportHandler(EntityType entityType) => entityType switch
        {
            EntityType.RecordLabel => serviceProvider.GetRequiredService<ImportHandlers.LabelImportHandler>(),
            EntityType.Artist => serviceProvider.GetRequiredService<ImportHandlers.ArtistImportHandler>(),
            _ => throw new Exception($"Unhandled {nameof(EntityType)}: {entityType}")
        };
    }
}
