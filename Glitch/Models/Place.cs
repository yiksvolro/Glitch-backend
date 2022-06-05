using Glitch.Extensions;
using System;
using System.Collections.Generic;

namespace Glitch.Models
{
    public class Place : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string WorkTime { get; set; }
        public string Rating { get; set; }
        public int AllTables { get; set; }
        public int FreeTables { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow.GetUkrainianDateTime();
        public DateTime UpdatedUtc  { get; set; } = DateTime.UtcNow.GetUkrainianDateTime();
        public string PhoneNumber { get; set; }
        public string InstagramName { get; set; }
        public string Email { get; set; }
        public virtual List<Table> Tables { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<File> Files { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
