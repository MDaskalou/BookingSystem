using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.DTO
{
    public class TreatmentTypeDto
    {
        public int TreatmentTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
      
    }

    public class  CreateTreatmentTypeDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
