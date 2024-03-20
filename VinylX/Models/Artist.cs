using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace VinylX.Models

{

    [Index(nameof(DiscogArtistId), IsUnique = true)]
    [Index(nameof(ArtistName), IsUnique = false)]
    public class Artist
    {
        public int ArtistId { get; set; }

        public required string ArtistName { get; set;}

        public int DiscogArtistId { get; set; }
    }
}
