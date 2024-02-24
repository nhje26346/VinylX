namespace VinylX.Models
{
    public class MasterRelease
    {
        public Guid MasterReleaseId { get; set; }
        public required string AlbumName { get; set; } 

        public int BarcodeNumber { get; set; }

        public Guid ArtistId { get; set; }
    }
}
