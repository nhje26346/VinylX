namespace VinylX.Models
{
    public class RecordLabel
    {
        public Guid RecordLabelId { get; set; }
        public required string LabelName { get; set; }
        public string? LabelSubdivision { get; set; }
    }
}
