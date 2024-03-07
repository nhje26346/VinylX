using Microsoft.EntityFrameworkCore;

namespace VinylX.Models
{
    [Index(nameof(DiscogMasterReleaseId), IsUnique = true)]
    public class MasterRelease
    {
        public int MasterReleaseId { get; set; }
        public required string AlbumName { get; set; } 

        public string? BarcodeNumber { get; set; }

        public string? CategoryNumber { get; set; }

        //public int ArtistId { get; set; }
        public required Artist Artist { get; set; }
        public int DiscogMasterReleaseId { get; set; }
    }
}
