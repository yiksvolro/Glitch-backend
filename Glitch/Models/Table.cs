using System;

namespace Glitch.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Capacity { get; set; }
        public int FreeSeats { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedUtc { get; set; }
        public virtual Place Place { get; set; }
        public int PlaceId { get; set; }
    }
}
