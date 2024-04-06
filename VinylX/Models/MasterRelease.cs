using Microsoft.EntityFrameworkCore;

namespace VinylX.Models
{
    [Index(nameof(DiscogMasterReleaseId), IsUnique = true)]
    [Index(nameof(AlbumName), IsUnique = false)]
    public class MasterRelease
    {
        public int MasterReleaseId { get; set; }
        public required string AlbumName { get; set; }

        public string? BarcodeNumber { get; set; }

        public required Artist Artist { get; set; }
        public int DiscogMasterReleaseId { get; set; }
    }
}
