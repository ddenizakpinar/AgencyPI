using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyPI.Models
{
    public abstract class DatedEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}