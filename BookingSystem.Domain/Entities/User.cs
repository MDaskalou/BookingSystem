using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
       
        public string Fullname { get; set; } 
       
        public string Email { get; set; } 
      
        public string PasswordHash { get; set; } 
        public bool IsDeleted { get; set; } = false;

        //navigation
        public Role Role  { get; set; }
        public int RoleId { get; set; }
    }
}
