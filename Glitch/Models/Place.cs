using System;
using System.Collections.Generic;

namespace Glitch.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc  { get; set; }
        public virtual List<Table> Tables { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
