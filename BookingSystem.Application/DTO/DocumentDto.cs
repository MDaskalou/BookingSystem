namespace BookingSystem.Application.DTO
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public bool Verified { get; set; } = false;
        public int UploadedById { get; set; }
    }

    public class CreateDocumentDto
    {
        public string FileName { get; set; } = string.Empty;
        public bool Verified { get; set; } = false;
        public int UploadedById { get; set; }
    }
}
