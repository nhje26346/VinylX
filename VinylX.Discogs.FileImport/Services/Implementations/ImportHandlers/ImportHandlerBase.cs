using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VinylX.Discogs.FileImport.Services.Contracts;

namespace VinylX.Discogs.FileImport.Services.Implementations.ImportHandlers
{
    internal abstract class ImportHandlerBase : IImportHandler
    {
        private readonly ILogger<ImportHandlerBase> logger;

        protected virtual int SingleMessageMaxSize => 45000000;

        protected abstract string ImportEndpointUrl { get; }

        public ImportHandlerBase(ILogger<ImportHandlerBase> logger)
        {
            this.logger = logger;
        }

        public async Task ImportFile(string filename)
        {
            var importSubFiles = await SplitFile(filename);
            
            logger.LogInformation("Sending {fileCount} files to endpoint {endpoint}...", importSubFiles.Count(), ImportEndpointUrl);

            var fileCounter = 0;

            using (var httpClient = new HttpClient())
            {
                foreach (var importSubFile in importSubFiles)
                {
                    fileCounter++;

                    var tooManyRequestWaitTime = 15000;
                    var retry = true;
                    while (retry)
                    {
                        retry = false;

                        var request = new HttpRequestMessage(new HttpMethod("POST"), ImportEndpointUrl);
                        request.Content = new StreamContent(File.OpenRead(importSubFile));
                        request.Content.Headers.TryAddWithoutValidation("Content-Type", "application/xml");

                        HttpResponseMessage response = null!;
                        try
                        {
                            logger.LogInformation("Sending HTTP request ({count} / {total})...", fileCounter, importSubFiles.Count());
                            response = await httpClient.SendAsync(request);
                            logger.LogInformation("Response received.");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Exception caught while sending file {file}", importSubFile);
                        }

                        if (!response.IsSuccessStatusCode)
                        {
                            logger.LogError("Service returned status code {code} {codeDesc}", (int)response.StatusCode, response.StatusCode);

                            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                tooManyRequestWaitTime = tooManyRequestWaitTime * 2;
                                logger.LogInformation("Waiting {seconds} seconds before retrying...", tooManyRequestWaitTime / 1000);
                                Thread.Sleep(tooManyRequestWaitTime);
                                retry = true;
                            }
                        }
                        else
                        {
                            logger.LogInformation("Transmission success: {code} {codeDesc}", (int)response.StatusCode, response.StatusCode);
                        }
                    }
                }
            }
        }

        private async Task<IEnumerable<string>> SplitFile(string filename)
        {
            var tempDir = Directory.CreateDirectory($"temp\\import\\{Guid.NewGuid()}");

            using var importStream = File.OpenRead(filename);
            using var xmlReader = CreateXmlReader(importStream);

            // Read until we hit the first element
            while (xmlReader.NodeType != XmlNodeType.Element && !xmlReader.EOF) { await xmlReader.ReadAsync(); }

            // Write to an envelope stream until we hit the first message
            // We will use the envelope to be applied to each individual message
            using var envelopeStream = new MemoryStream();
            using var envelopeWriter = CreateXmlWriter(envelopeStream);
            await DoWriteWhile(xmlReader, envelopeWriter, r => !IsAtNewMessage(r));

            // Create a function for writing the end element tags correlating to the unclosed elements from the envelope start
            var trailerEndElementCount = xmlReader.Depth;
            Func<XmlWriter, Task> writeTrailer = async w =>
            {
                for (int i = 0; i < trailerEndElementCount; i++)
                {
                    await w.WriteEndElementAsync();
                }
            };

            // Write the trailer to the envelope stream since it needs to be well-formed
            await writeTrailer(envelopeWriter);    
            await envelopeWriter.FlushAsync();


            // Create a function that creates a stream and a writer and writes the opening part of the envelope the the stream
            Func<Task<(Stream, XmlWriter)>> getNext = async () =>
            {
                var tempStream = GetTempFileStream(tempDir.FullName);
                var tempWriter = CreateXmlWriter(tempStream);
                // Rewind the envelope stream
                envelopeStream.Position = 0;
                // Create a reader for reading the envelope that is disposed within the scope of this function.
                using var headerReader = CreateXmlReader(envelopeStream);
                // Write to the new stream until we meet the trailing part of the envelope.
                await DoWriteWhile(headerReader, tempWriter, r => !IsAtTrailer(r));
                return (tempStream, tempWriter);
            };

            // Get the first stream and writer from the function above
            Stream currentStream = null!;
            XmlWriter currentWriter = null!;
            (currentStream, currentWriter) = await getNext();
            

            logger.LogInformation("Start preparing the first message.");

            while (!IsAtTrailer(xmlReader))
            {
                // We are currently at the start of a message, so we can write the current node
                await currentWriter.WriteNodeAsync(xmlReader, false);
                while (!IsAtNewMessage(xmlReader) && !IsAtTrailer(xmlReader))
                {
                    // If we are not at a new message or at the trailer we can write the current node
                    // This is usually whitespaces between elements
                    await currentWriter.WriteNodeAsync(xmlReader, false);
                }
                if (IsAtNewMessage(xmlReader))
                {
                    // New message - call flush to be able to check the size of the current stream,
                    // so that we can check if we have exeeded the max limit.
                    // If not we keep writing the the same stream.
                    await currentWriter.FlushAsync();
                    if (currentStream.Position > SingleMessageMaxSize)
                    {
                        // We have reached the max size limit
                        // Write envelope trailer to current stream
                        await writeTrailer(currentWriter);

                        // Dispose current stream and writer
                        await currentWriter.DisposeAsync();
                        await currentStream.DisposeAsync();

                        // Get next stream and writer to work with
                        logger.LogInformation("Starting new file, since the current exeeded the max size of {maxSize}. Approximately {percent}% of the input file has been processed.", SingleMessageMaxSize, importStream.Position * 100 / importStream.Length);
                        (currentStream, currentWriter) = await getNext();
                    }
                }
                else if (IsAtTrailer(xmlReader))
                {
                    // We have hit the trailer, so this is the last message
                    // // Write envelope trailer to current stream
                    await writeTrailer(currentWriter);

                    // Dispose current stream and writer
                    await currentWriter.DisposeAsync();
                    await currentStream.DisposeAsync();
                }
            }

            return tempDir.GetFiles().Select(f => f.FullName);
        }

        protected virtual bool IsAtNewMessage(XmlReader xmlReader) => xmlReader.NodeType == XmlNodeType.Element && xmlReader.Depth == 1;

        protected virtual bool IsAtTrailer(XmlReader xmlReader) => xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Depth == 0;

        private XmlWriter CreateXmlWriter(Stream stream) => XmlWriter.Create(stream, new XmlWriterSettings { Async = true, OmitXmlDeclaration = true });

        private XmlReader CreateXmlReader(Stream stream) => XmlReader.Create(stream, new XmlReaderSettings { Async = true });

        private async Task DoWriteWhile(XmlReader reader, XmlWriter writer, Func<XmlReader, bool> condition)
        {
            if (reader.EOF)
            {
                return;
            }
            if (reader.NodeType == XmlNodeType.None)
            {
                await reader.ReadAsync();
            }
            if (reader.EOF)
            {
                return;
            }

            do 
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        await writer.WriteStartElementAsync(reader.Prefix, reader.LocalName, reader.NamespaceURI);
                        reader.ReadStartElement();
                    }
                }
                else
                {
                    await writer.WriteNodeAsync(reader, false);
                }

                await reader.ReadAsync();
            } 
            while (!reader.EOF && condition(reader));
        }

        private Stream GetTempFileStream(string tempDir) => File.OpenWrite(Path.Combine(tempDir, $"{Guid.NewGuid()}.xml"));
    }
}
