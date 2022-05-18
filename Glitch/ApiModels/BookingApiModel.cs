using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.ApiModels
{
    public class BookingApiModel
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int TableId { get; set; }
        public DateTime BookedOn { get; set; }
    }
}
