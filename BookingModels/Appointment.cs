using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingModels
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public string CompanyNotes { get; set; }
        public string CustomerNotes { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeFrom { get; set; } 
        public TimeOnly TimeTo { get; set; }
    }
}
