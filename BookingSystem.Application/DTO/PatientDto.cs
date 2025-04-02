using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.DTO
{
    
        public class PatientDto //get dto
        {
            public int PatientId { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string SocialSecurityNumber { get; set; } = string.Empty;
        }
    

    public class CreatePatientDto //post and put dto
    {
        public string FullName { get; set; } = string.Empty;
        public string SocialSecurityNumber { get; set; } = string.Empty;
    }
}
