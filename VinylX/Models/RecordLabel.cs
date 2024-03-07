using Microsoft.EntityFrameworkCore;

namespace VinylX.Models
{
    [Index(nameof(DiscogLabelId), IsUnique = true)]
    public class RecordLabel
    {
        public int RecordLabelId { get; set; }
        public required string LabelName { get; set; }
        
        public int DiscogLabelId { get; set; }
    }
}
