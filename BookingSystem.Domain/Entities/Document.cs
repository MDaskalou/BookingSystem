using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public bool Verified { get; set; } = false;

        public int UploadedByUserId { get; set; }
        public User UploadedBy { get; set; } = null!;
    }
}
