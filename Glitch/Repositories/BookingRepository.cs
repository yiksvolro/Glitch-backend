using AutoMapper;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glitch.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(GlitchContext glitchContext, ILogger<BaseRepository<Booking>> logger, IMapper mapper) : base(glitchContext, logger, mapper)
        {
        }
    }
}
