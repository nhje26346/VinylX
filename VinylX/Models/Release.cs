using Microsoft.EntityFrameworkCore;

namespace VinylX.Models
{
    [Index(nameof(DiscogReleaseId), IsUnique = true)]
    [Index(nameof(Edition), IsUnique = false)]
    public class Release
    {
        public int ReleaseId { get; set; }
        public string? ReleaseDate { get; set; }
        public string? CategoryNumber { get; set; }
        public string? Edition { get; set; }
        public string? BarcodeNumber { get; set; }

        public required MasterRelease MasterRelease { get; set; }

        public string? Genre { get; set; }

        public int DiscogReleaseId { get; set; }

        public RecordLabel? RecordLabel { get; set; }

        public required Media Media { get; set; }
    }
}
