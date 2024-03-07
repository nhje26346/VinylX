using Microsoft.EntityFrameworkCore;

namespace VinylX.Models

{
    [Index(nameof(DiscogArtistId), IsUnique = true)]
    public class Artist
    {
        public int ArtistId { get; set; }

        public required string ArtistName { get; set;}

        public int DiscogArtistId { get; set; }
    }
}
