namespace VinylX.Models
{
    public class MasterRelease
    {
        public int MasterReleaseId { get; set; }
        public required string AlbumName { get; set; } 

        public int BarcodeNumber { get; set; }

        public int ArtistId { get; set; }
    }
}
