using Glitch.Extensions;
using System;

namespace Glitch.Models
{
    public class Booking : IBaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int TableId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.GetUkrainianDateTime();
        public DateTime BookedOn { get; set; }
        public bool IsArchive { get; set; } = false;
        public virtual User User { get; set; }
        public virtual Place Place { get; set; }
        public virtual Table Table { get; set; }
    }
}
