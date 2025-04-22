using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.DTO
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public string Message { get; set; } 
        public DateTime SentAt { get; set; }
        public int RecipientId { get; set; }
    }
   
        public class CreateNotificationDto
        {
            public string Message { get; set; }
            public int RecipientId { get; set; }
        }
    

}
