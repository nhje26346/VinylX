namespace VinylX.Models
    
{
    public class Folder
    {
        public int FolderId { get; set; }
        public required string FolderName { get; set; }

        public required User User { get; set; }
    }
}
