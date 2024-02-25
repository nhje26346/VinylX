namespace VinylX.Models
{
    public class RecordLabel
    {
        public int RecordLabelId { get; set; }
        public required string LabelName { get; set; }
        
        public int DiscogLabelId { get; set; }
    }
}
