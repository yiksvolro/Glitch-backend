using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Glitch.Models
{
    public class User : IdentityUser<int>, IBaseModel
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public virtual List<Place> Places { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
