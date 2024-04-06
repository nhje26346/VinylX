using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylX.Discogs.FileImport.Services.Implementations
{
    internal class ZipFileService
    {
        private static string[] zipExtensions => new[] { ".zip" };
        private static string[] gZipExtensions => new[] { ".gz" };

        private readonly ILogger<ZipFileService> logger;

        // Helper Service to eliminate the need for unzipping.
        public ZipFileService(ILogger<ZipFileService> logger)
        {
            this.logger = logger;
        }

        public Task<string> UnzipIfZipped(string filename)
        {
            if (zipExtensions.Contains(Path.GetExtension(filename)?.ToLower()))
            {
                logger.LogInformation("Extracting archive {file}...", filename);
                var tempFolder = Directory.CreateDirectory($"temp\\unzip\\{Guid.NewGuid()}");
                ZipFile.ExtractToDirectory(filename, tempFolder.FullName, false);
                return Task.FromResult(tempFolder.GetFiles().Single().FullName);
            }
            if (gZipExtensions.Contains(Path.GetExtension(filename)?.ToLower()))
            {
                logger.LogInformation("Extracting archive {file}...", filename);
                var tempFolder = Directory.CreateDirectory($"temp\\unzip\\{Guid.NewGuid()}");
                using var compressedFileStream = File.Open(filename, FileMode.Open);
                var outputFilename = Path.Combine(tempFolder.FullName, Path.GetFileNameWithoutExtension(filename));
                using var outputFileStream = File.Create(outputFilename);
                using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
                decompressor.CopyTo(outputFileStream);
                return Task.FromResult(outputFilename);
            }
            return Task.FromResult(filename);
        }
    }
}
