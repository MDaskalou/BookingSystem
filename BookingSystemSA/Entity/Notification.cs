using BookingSystemSA.Entity;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public int RecipientId { get; set; }
        public User Recipient { get; set; } = null!;
    }
}
