namespace VinylX.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
