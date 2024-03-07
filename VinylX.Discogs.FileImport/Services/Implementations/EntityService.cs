using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Discogs.FileImport.Entities;

namespace VinylX.Discogs.FileImport.Services.Implementations
{
    internal class EntityService
    {
        private readonly Dictionary<EntityType, Func<string, bool>> checkFunctions;

        public EntityService()
        {
            checkFunctions = new Dictionary<EntityType, Func<string, bool>>();
            checkFunctions[EntityType.RecordLabel] = fn => fn.Contains("_labels.");
            checkFunctions[EntityType.Artist] = fn => fn.Contains("_artists.");
        }

        public EntityType GetEntityTypeFromFilename(string filename)
        {
            var localFilename = Path.GetFileName(filename);
            foreach (var func in checkFunctions)
            {
                if (func.Value(localFilename))
                {
                    return func.Key;
                }
            }
            throw new Exception($"Could not determine entity type based on filename {localFilename}");
        }
    }
}
