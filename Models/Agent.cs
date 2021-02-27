using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyPI.Models
{
    public class Agent : DatedEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string WorkingArea { get; set; }
        public int Commission { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Customer> Customers { get; set; }
    }
}