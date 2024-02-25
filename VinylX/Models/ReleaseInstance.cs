namespace VinylX.Models
{
    public class ReleaseInstance
    {
    public int ReleaseInstanceId { get; set; }

    public string? Quality { get; set; }
    public required Folder Folder { get; set; }

    public required Release Release { get; set; }

    }
}
