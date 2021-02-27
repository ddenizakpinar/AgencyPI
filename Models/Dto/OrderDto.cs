using System;
using System.ComponentModel.DataAnnotations;

namespace AgencyPI.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual Customer Customer { get; set; }
    }
}