using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VinylX.Discogs.FileImport.Extensions
{
    internal static class XmlReaderExtensions
    {
        public static async Task<bool> ReadAsync(this XmlReader reader, bool readElementRecursive)
        {
            if (reader.EOF)
            {
                return false;
            }

            if (readElementRecursive && reader.NodeType == XmlNodeType.Element)
            {
                var result = false;
                var currentDepth = reader.Depth;
                result = await reader.ReadAsync();
                while (reader.Depth > currentDepth || (reader.Depth == currentDepth && reader.NodeType == XmlNodeType.EndElement))
                {
                    await reader.ReadAsync();
                }
                return result;
            }
            else if (!readElementRecursive && reader.NodeType == XmlNodeType.Element)
            {
                if (reader.MoveToFirstAttribute())
                {
                    return true;
                }
                return await reader.ReadAsync();
            }
            else if (reader.NodeType == XmlNodeType.Attribute)
            {
                if (reader.MoveToNextAttribute())
                {
                    return true;
                }
                return await reader.ReadAsync();
            }
            else 
            {
                return await reader.ReadAsync();
            }
        }
    }
}
