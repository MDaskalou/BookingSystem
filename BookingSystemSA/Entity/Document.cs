using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public bool Verified { get; set; } = false;

        public int UploadedById { get; set; }
        public User UploadedBy { get; set; } = null!;
    }
}
