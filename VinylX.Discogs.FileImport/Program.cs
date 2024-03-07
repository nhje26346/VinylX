using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VinylX.Discogs.FileImport.Services.Contracts;
using VinylX.Discogs.FileImport.Services.Implementations;
using VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers;

public static class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(logging => logging.AddConsole())
            .AddScoped<FileImportService>()
            .AddScoped<EntityService>()
            .AddScoped<ZipFileService>()
            .AddScoped<IImportHandlerFactory, ImportHandlerFactory>()
            .AddScoped<LabelImportHandler>()
            .AddScoped<ArtistImportHandler>()
            .BuildServiceProvider();

        serviceProvider.GetRequiredService<FileImportService>().RunImport(args).GetAwaiter().GetResult();

        Console.ReadLine();
    }
}