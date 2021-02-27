using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgencyPI.Models
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string WorkingArea { get; set; }
        public int Grade { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}