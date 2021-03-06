using System;
using System.Collections.Generic;

namespace Glitch.Models
{
    public class Table : IBaseModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public string Transform { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedUtc { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
        public DateTime UpdatedUtc { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
        public virtual List<Booking> Bookings { get; set; }
        public virtual Place Place { get; set; }
        public int PlaceId { get; set; }
    }
}
