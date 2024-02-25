namespace VinylX.Models
{
    public class Release
    {
        public int ReleaseId { get; set; }
        public string? ReleaseDate { get; set; }
        public string? CategoryNumber { get; set; }
        public string? Edition { get; set; }

        public string? Genre { get; set; }

        public int DiscogReleaseId { get; set; }
    }
}
