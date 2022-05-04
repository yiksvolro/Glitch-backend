using System;

namespace Glitch.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int TableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime BookedOn { get; set; }
        public virtual User User { get; set; }
        public virtual Place Place { get; set; }
        public virtual Table Table { get; set; }
    }
}
