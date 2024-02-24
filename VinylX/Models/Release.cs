namespace VinylX.Models
{
    public class Release
    {
        public Guid ReleaseId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CategoryNumber { get; set; }
        public string? Edition { get; set; }

        public string? Genre { get; set; }
    }
}
